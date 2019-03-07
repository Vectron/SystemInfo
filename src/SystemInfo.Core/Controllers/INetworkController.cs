using System;
using System.Collections.Generic;

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