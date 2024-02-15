using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reactive.Linq;
using SystemInfo.Core.Poco;

namespace SystemInfo.Core.Controllers;

/// <summary>
/// Implementation of <see cref="INetworkController"/>.
/// </summary>
public class NetworkController : INetworkController
{
    private readonly Dictionary<string, (long Send, long Received, DateTime DateTime)> previous = new(StringComparer.Ordinal);

    /// <inheritdoc/>
    public IObservable<IEnumerable<NetworkData>> NetworkUse
        => Observable
        .Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
        .Select(_ => GetData())
        .Publish()
        .RefCount();

    private static IEnumerable<NetworkInterface> GetInterfaces
        => NetworkInterface
        .GetAllNetworkInterfaces()
        .Where(x => x.OperationalStatus == OperationalStatus.Up
            && (x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
                || x.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet
                || x.NetworkInterfaceType == NetworkInterfaceType.FastEthernetT
                || x.NetworkInterfaceType == NetworkInterfaceType.FastEthernetFx
                || x.NetworkInterfaceType == NetworkInterfaceType.Ethernet));

    private List<NetworkData> GetData()
    {
        var result = new List<NetworkData>();

        foreach (var networkInterface in GetInterfaces)
        {
            if (previous.TryGetValue(networkInterface.Name, out (long, long, DateTime) sendReceived))
            {
                var totalBytesSend = networkInterface.GetIPStatistics().BytesSent;
                var totalBytesReceived = networkInterface.GetIPStatistics().BytesReceived;
                var currenTime = DateTime.Now;

                var totalSeconds = (currenTime - sendReceived.Item3).TotalSeconds;

                var bytesSend = totalBytesSend - sendReceived.Item1;
                var bytesSendPerSecond = bytesSend / totalSeconds;

                var bytesReceived = totalBytesReceived - sendReceived.Item2;
                var bytesReceivedPerSecond = bytesReceived / totalSeconds;

                previous[networkInterface.Name] = (totalBytesSend, totalBytesReceived, currenTime);
                var data = new NetworkData(networkInterface.Name, (ulong)bytesSendPerSecond, (ulong)bytesReceivedPerSecond);
                result.Add(data);
            }
            else
            {
                var totalBytesSend = networkInterface.GetIPStatistics().BytesSent;
                var totalBytesReceived = networkInterface.GetIPStatistics().BytesReceived;
                var currenTime = DateTime.Now;
                previous.Add(networkInterface.Name, (totalBytesSend, totalBytesReceived, currenTime));
            }
        }

        return result;
    }
}
