using EVESharp.Database;
using EVESharp.Database.Extensions;
using EVESharp.Database.Market;
using EVESharp.Database.Old;
using EVESharp.EVE.Data.Inventory;
using EVESharp.EVE.Data.Inventory.Items.Types;
using EVESharp.EVE.Market;
using EVESharp.EVE.Network.Services;
using EVESharp.EVE.Network.Services.Validators;
using EVESharp.Types;

namespace EVESharp.Node.Services.Characters;

public class charmgr : Service
{
    public override AccessLevel         AccessLevel => AccessLevel.None;
    private         OldCharacterDB      DB          { get; }
    private         MarketDB            MarketDB    { get; }
    private         IItems              Items       { get; }
    private         IWallets            Wallets     { get; }
    private         IDatabase Database    { get; }

    public charmgr (IDatabase database, OldCharacterDB db, MarketDB marketDB, IItems items, IWallets wallets)
    {
        Database     = database;
        DB           = db;
        MarketDB     = marketDB;
        this.Items   = items;
        this.Wallets = wallets;
    }

    public PyDataType GetPublicInfo (ServiceCall call, PyInteger characterID)
    {
        return Database.ChrGetPublicInfo (characterID);
    }

    [MustBeCharacter]
    public PyDataType GetPublicInfo3 (ServiceCall call, PyInteger characterID)
    {
        return Database.ChrGetPublicInfo3 (characterID);
    }

    [MustBeCharacter]
    public PyDataType GetTopBounties (ServiceCall call)
    {
        return DB.GetTopBounties ();
    }

    [MustBeCharacter]
    public PyDataType AddToBounty (ServiceCall call, PyInteger characterID, PyInteger bounty)
    {
        // access the wallet and do the required changes
        using IWallet wallet = this.Wallets.AcquireWallet (call.Session.CharacterID, WalletKeys.MAIN);
        {
            // ensure the character has enough balance
            wallet.EnsureEnoughBalance (bounty);
            // take the balance from the wallet
            wallet.CreateJournalRecord (MarketReference.Bounty, null, characterID, -bounty, "Added to bounty price");
        }

        // create the bounty record and update the information in the database
        DB.AddToBounty (call.Session.CharacterID, characterID, bounty);

        return null;
    }

    [MustBeCharacter]
    public PyDataType GetPrivateInfo (ServiceCall call, PyInteger characterID)
    {
        return DB.GetPrivateInfo (characterID);
    }
}