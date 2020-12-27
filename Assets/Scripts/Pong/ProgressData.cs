using System;

[Serializable]
public class ProgressData
{
    // Start is called before the first frame update

    public int coreLevel = 0;
    public int bitLevel = 0;

    int[] coreTypes = { 1, 2, 4, 8, 16 };
    int[] bitTypes = { 4, 8, 16, 32, 64 };

    public int numCores = 1;
    public int numBits = 4;

    public void AddCoreLevel(int level)
    {
        this.coreLevel += level;
        numCores = coreTypes[this.coreLevel];
    }

    public void AddBitLevel(int level)
    {
        this.bitLevel += level;
        numBits = bitTypes[this.bitLevel];
    }
}
