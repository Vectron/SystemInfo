using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reactive.Linq;
using SystemInfo.Core.Poco;

namespace SystemInfo.Core.Controllers
{
    public class NetworkController : INetworkController
    {
        private readonly Dictionary<string, (long send, long received)> previous = new Dictionary<string, (long, long)>();

        public IObservable<IEnumerable<NetworkData>> NetworkUse
                    => Observable
            .Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .Select(_ => GetData())
            .Publish()
            .RefCount();

        private IEnumerable<NetworkData> GetData()
        {
            var networkInterfaces = NetworkInterface
                      .GetAllNetworkInterfaces()
                      .Where(x => x.OperationalStatus == OperationalStatus.Up)
                      .Where(x => x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
                          || x.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet
                          || x.NetworkInterfaceType == NetworkInterfaceType.FastEthernetT
                          || x.NetworkInterfaceType == NetworkInterfaceType.FastEthernetFx
                          || x.NetworkInterfaceType == NetworkInterfaceType.Ethernet);

            var result = new List<NetworkData>();

            foreach (var networkInterface in networkInterfaces)
            {
                if (previous.TryGetValue(networkInterface.Name, out (long, long) sendReceived))
                {
                    var bytesSend = networkInterface.GetIPStatistics().BytesSent;
                    var bytesReceived = networkInterface.GetIPStatistics().BytesReceived;
                    var data = new NetworkData(
                                    networkInterface.Name,
                                    (ulong)(bytesSend - sendReceived.Item1),
                                    (ulong)(bytesReceived - sendReceived.Item2));

                    sendReceived.Item1 = bytesSend;
                    sendReceived.Item2 = bytesReceived;
                    result.Add(data);
                }
                else
                {
                    previous.Add(networkInterface.Name, (networkInterface.GetIPStatistics().BytesSent, networkInterface.GetIPStatistics().BytesReceived));
                }
            }

            return result;
        }
    }
}