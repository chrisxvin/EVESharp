namespace EVESharp.Types;

public class PyChecksumedStream : PyDataType
{
    public PyDataType Data { get; }

    public PyChecksumedStream (PyDataType data)
    {
        this.Data = data;
    }

    public override int GetHashCode ()
    {
        if (this.Data is null)
            return 0x24521455;

        return this.Data.GetHashCode () ^ 0x24521455; // some random magic number to spread the hashcode
    }
}