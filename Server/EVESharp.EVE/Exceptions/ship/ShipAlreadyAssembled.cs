﻿using EVESharp.EVE.Data.Inventory;
using EVESharp.EVE.Packets.Exceptions;
using EVESharp.PythonTypes.Types.Collections;

namespace EVESharp.EVE.Exceptions.ship;

public class ShipAlreadyAssembled : UserError
{
    public ShipAlreadyAssembled (Type type) : base ("ShipAlreadyAssembled", new PyDictionary {["type"] = FormatTypeIDAsName (type.ID)}) { }
}