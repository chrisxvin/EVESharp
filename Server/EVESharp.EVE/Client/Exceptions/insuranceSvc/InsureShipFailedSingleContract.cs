﻿using EVESharp.EVE.Packets.Exceptions;
using EVESharp.PythonTypes.Types.Collections;

namespace EVESharp.EVE.Client.Exceptions.insuranceSvc;

public class InsureShipFailedSingleContract : UserError
{
    public InsureShipFailedSingleContract (int ownerID) : base (
        "InsureShipFailedSingleContract",
        new PyDictionary {["ownerName"] = FormatOwnerID (ownerID)}
    ) { }
}