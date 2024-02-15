using System;
using SystemInfo.Core.Poco;

namespace SystemInfo.Core.Controllers
{
    /// <summary>
    /// A controller for monitoring memory use.
    /// </summary>
    public interface IMemoryController
    {
        /// <summary>
        /// Gets an <see cref="IObservable{T}"/> containing the memory use.
        /// </summary>
        IObservable<UsageData> MemoryUse
        {
            get;
        }
    }
}
