﻿using EVESharp.Types.Collections;
using NUnit.Framework;

namespace EVESharp.Types.Unit.Types.Primitives;

public class PyChecksumedStreamTests
{
    public static readonly PyList list = new PyList() { 5000000, 15000000, 150.0 };
    public static readonly PyTuple tuple1 = new PyTuple(3) { [0] = list, [1] = list, [2] = list };
    public static readonly PyTuple tuple2 = new PyTuple(1) {[0] = list};
    
    [Test]
    public void ChecksumedStreamComparison()
    {
        PyChecksumedStream obj1 = new PyChecksumedStream(tuple1);
        PyChecksumedStream obj2 = new PyChecksumedStream(tuple2);
        PyChecksumedStream obj3 = new PyChecksumedStream(tuple1);
        PyChecksumedStream obj4 = null;

        Assert.True(obj1 == obj3);
        Assert.False(obj1 == obj2);
        Assert.False(obj1 != obj3);
        Assert.True(obj1 != obj2);
        
        Assert.False(obj1 == null);
        Assert.True(obj1 != null);
        Assert.False(obj1 is null);
        Assert.True(obj1 is not null);
        Assert.True(obj4 == null);
        Assert.False(obj4 != null);
        Assert.True(obj4 is null);
        Assert.False(obj4 is not null);
        Assert.False(obj1 == obj4);
        Assert.True(obj1 != obj4);
    }
}