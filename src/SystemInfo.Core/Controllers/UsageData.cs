namespace SystemInfo.Core.Controllers
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

        public int Percantage
            => Utils.FloatToPercent((Size - Available) / Size);

        public ulong Size
        {
            get;
        }

        public ulong TotalUsed
            => Size - Available;
    }
}