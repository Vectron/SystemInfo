using System;
using System.Collections.Generic;

namespace SystemInfo.Core.Controllers
{
    public interface ICPUController
    {
        IObservable<IEnumerable<float>> CoreUse
        {
            get;
        }

        IObservable<float> TotalCpuUse
        {
            get;
        }
    }
}