using System;

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