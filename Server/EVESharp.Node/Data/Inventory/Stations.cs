using System.Collections.Generic;
using EVESharp.Database;
using EVESharp.Database.Extensions.Inventory;
using EVESharp.Database.Inventory.Stations;
using EVESharp.EVE.Data.Inventory;
using EVESharp.EVE.Data.Inventory.Items.Types;
using Type = EVESharp.Database.Inventory.Stations.Type;

namespace EVESharp.Node.Data.Inventory;

public class Stations : Dictionary <int, Station>, IStations
{
    public Dictionary <int, Operation> Operations   { get; }
    public Dictionary <int, Type>      StationTypes { get; }
    public Dictionary <int, string>    Services     { get; }

    public Stations (IDatabase Database)
    {
        this.Operations   = Database.StaLoadOperations ();
        this.StationTypes = Database.StaLoadStationTypes ();
        this.Services     = Database.StaLoadServices ();
    }
}