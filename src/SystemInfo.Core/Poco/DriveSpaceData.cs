namespace SystemInfo.Core.Poco
{
    /// <summary>
    /// Represents the <see cref="UsageData"/> of an hard drive.
    /// </summary>
    public class DriveSpaceData : UsageData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DriveSpaceData"/> class.
        /// </summary>
        /// <param name="name">The name of the drive.</param>
        /// <param name="size">The total size.</param>
        /// <param name="available">The available size.</param>
        public DriveSpaceData(string name, ulong size, ulong available)
            : base(size, available) => Name = name;

        /// <summary>
        /// Gets the name of this drive.
        /// </summary>
        public string Name
        {
            get;
        }
    }
}
