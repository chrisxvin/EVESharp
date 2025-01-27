﻿using EVESharp.Types.Collections;

namespace EVESharp.EVE.Exceptions.facWarMgr;

public class FactionCharJoinDenied : UserError
{
    public FactionCharJoinDenied (string reason, int hoursLeft) : base (
        "FactionCharJoinDenied", new PyDictionary
        {
            ["reason"] = reason,
            ["hours"]  = hoursLeft
        }
    ) { }
}