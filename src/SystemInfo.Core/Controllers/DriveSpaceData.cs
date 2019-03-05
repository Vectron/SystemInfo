namespace SystemInfo.Core.Controllers
{
    public class DriveSpaceData : UsageData
    {
        public DriveSpaceData(string name, ulong size, ulong available)
            : base(size, available)
        {
            Name = name;
        }

        public string Name
        {
            get;
        }
    }
}