using System;
using System.Collections.Generic;

namespace SystemInfo.Core.Controllers
{
    /// <summary>
    /// A controller for monitoring the cpu load.
    /// </summary>
    public interface ICPUController
    {
        /// <summary>
        /// Gets an <see cref="IObservable{T}"/> returning the load on every core.
        /// </summary>
        IObservable<IEnumerable<float>> CoreUse
        {
            get;
        }

        /// <summary>
        /// Gets an <see cref="IObservable{T}"/> returning the total load on the cpu.
        /// </summary>
        IObservable<float> TotalCpuUse
        {
            get;
        }
    }
}
