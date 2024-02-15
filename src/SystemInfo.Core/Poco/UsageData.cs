namespace SystemInfo.Core.Poco
{
    /// <summary>
    /// Represent usage data of an resource.
    /// </summary>
    public class UsageData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsageData"/> class.
        /// </summary>
        /// <param name="size">The total size.</param>
        /// <param name="available">The total amount available.</param>
        public UsageData(ulong size, ulong available)
        {
            Size = size;
            Available = available;
        }

        /// <summary>
        /// Gets the total available amount.
        /// </summary>
        public ulong Available
        {
            get;
        }

        /// <summary>
        /// Gets the available amount in percentages.
        /// </summary>
        public float Percentage
            => Size == 0 ? 0 : (Size - Available) / (float)Size * 100;

        /// <summary>
        /// Gets the total size of this resource.
        /// </summary>
        public ulong Size
        {
            get;
        }

        /// <summary>
        /// Gets the used size of this resource.
        /// </summary>
        public ulong TotalUsed
            => Size - Available;
    }
}
