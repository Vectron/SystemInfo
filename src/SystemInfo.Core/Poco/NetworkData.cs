namespace SystemInfo.Core.Poco
{
    public class NetworkData
    {
        public NetworkData(string name, ulong send, ulong received)
        {
            Name = name;
            Send = send;
            Received = received;
        }

        public string Name
        {
            get;
        }

        public ulong Received
        {
            get;
            internal set;
        }

        public ulong Send
        {
            get;
            internal set;
        }
    }
}