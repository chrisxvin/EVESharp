﻿using EVESharp.Types.Collections;

namespace EVESharp.EVE.Exceptions.marketProxy;

public class MktOrderDelay : UserError
{
    public MktOrderDelay (long delay) : base ("MktOrderDelay", new PyDictionary {["delay"] = FormatShortTime (delay)}) { }
}