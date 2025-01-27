﻿using System.Collections.Generic;
using EVESharp.EVE.Packets.Complex;
using EVESharp.EVE.Sessions;
using EVESharp.Types;
using EVESharp.Types.Collections;

namespace EVESharp.EVE.Notifications.Station;

public class OnCharNoLongerInStation : ClientNotification
{
    private const string NOTIFICATION_NAME = "OnCharNoLongerInStation";

    public int? CharacterID   { get; init; }
    public int? CorporationID { get; init; }
    public int? AllianceID    { get; init; }
    public int? WarFactionID  { get; init; }

    public OnCharNoLongerInStation (Session session) : base (NOTIFICATION_NAME)
    {
        this.CharacterID   = session.CharacterID;
        this.CorporationID = session.CorporationID;
        this.AllianceID    = session.AllianceID;
        this.WarFactionID  = session.WarFactionID;
    }

    public override List <PyDataType> GetElements ()
    {
        return new List <PyDataType>
        {
            new PyTuple (4)
            {
                [0] = this.CharacterID,
                [1] = this.CorporationID,
                [2] = this.AllianceID,
                [3] = this.WarFactionID
            }
        };
    }
}