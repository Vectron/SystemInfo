namespace SystemInfo.Core.Poco
{
    public class UsageData
    {
        public UsageData(ulong size, ulong available)
        {
            Size = size;
            Available = available;
        }

        public ulong Available
        {
            get;
        }

        public float Percantage
            => ((Size - Available) / (float)Size) * 100;

        public ulong Size
        {
            get;
        }

        public ulong TotalUsed
            => Size - Available;
    }
}