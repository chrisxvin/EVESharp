﻿using System.Collections.Generic;
using EVESharp.Database;
using EVESharp.Database.Dogma;
using EVESharp.Database.Extensions.Inventory;
using EVESharp.EVE.Data.Inventory;
using Serilog;

namespace EVESharp.Node.Dogma;

public class Expressions : Dictionary <int, Expression>, IExpressions
{
    public Expressions (IDatabase Database, ILogger Log) : base (Database.InvDgmLoadExpressions (Log))
    {
        Log.Debug ($"Loaded {this.Count} expressions for Dogma");
    }
}