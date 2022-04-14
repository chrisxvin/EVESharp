﻿using EVESharp.EVE.Packets.Exceptions;
using EVESharp.EVE.StaticData.Inventory;
using EVESharp.PythonTypes.Types.Collections;

namespace EVESharp.EVE.Client.Exceptions.ship;

public class ShipAlreadyAssembled : UserError
{
    public ShipAlreadyAssembled (Type type) : base ("ShipAlreadyAssembled", new PyDictionary {["type"] = FormatTypeIDAsName (type.ID)}) { }
}