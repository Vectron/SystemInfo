namespace SystemInfo.Core.Poco
{
    /// <summary>
    /// Container for network status.
    /// </summary>
    public class NetworkData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkData"/> class.
        /// </summary>
        /// <param name="name">THe name of the connection.</param>
        /// <param name="send">The amount send.</param>
        /// <param name="received">The amount received.</param>
        public NetworkData(string name, ulong send, ulong received)
        {
            Name = name;
            Send = send;
            Received = received;
        }

        /// <summary>
        /// Gets the name of this interface.
        /// </summary>
        public string Name
        {
            get;
        }

        /// <summary>
        /// Gets the total data received since last update.
        /// </summary>
        public ulong Received
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the total data send since last update.
        /// </summary>
        public ulong Send
        {
            get;
            internal set;
        }
    }
}
