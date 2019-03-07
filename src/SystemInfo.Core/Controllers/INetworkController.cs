using System;
using System.Collections.Generic;
using SystemInfo.Core.Poco;

namespace SystemInfo.Core.Controllers
{
    public interface INetworkController
    {
        IObservable<IEnumerable<NetworkData>> NetworkUse
        {
            get;
        }
    }
}