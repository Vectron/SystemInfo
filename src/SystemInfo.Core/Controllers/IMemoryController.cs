using System;
using SystemInfo.Core.Poco;

namespace SystemInfo.Core.Controllers
{
    public interface IMemoryController
    {
        IObservable<UsageData> MemoryUse
        {
            get;
        }
    }
}