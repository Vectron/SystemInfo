using System;
using System.Collections.Generic;
using SystemInfo.Core.Poco;

namespace SystemInfo.Core.Controllers
{
    public interface IDriveSpaceController
    {
        IObservable<IEnumerable<DriveSpaceData>> HDDUse
        {
            get;
        }

        IObservable<UsageData> TotalHDDUse
        {
            get;
        }
    }
}