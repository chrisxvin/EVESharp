﻿using EVESharp.EVE.Packets.Exceptions;

namespace EVESharp.EVE.Client.Exceptions.skillMgr;

public class RespecAttributesTooLow : UserError
{
    public RespecAttributesTooLow () : base ("RespecAttributesTooLow") { }
}