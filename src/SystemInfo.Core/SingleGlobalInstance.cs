using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace SystemInfo.Core
{
    /// <summary>
    /// Helper for only starting the application one time.
    /// </summary>
    public sealed class SingleGlobalInstance : IDisposable
    {
        private readonly Mutex mutex;
        private readonly TimeSpan timeOut;
        private bool hasHandle;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleGlobalInstance"/> class.
        /// </summary>
        public SingleGlobalInstance()
            : this(Timeout.InfiniteTimeSpan, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleGlobalInstance"/> class.
        /// </summary>
        /// <param name="id">A guid to monitor.</param>
        public SingleGlobalInstance(string id)
            : this(Timeout.InfiniteTimeSpan, id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleGlobalInstance"/> class.
        /// </summary>
        /// <param name="timeOut">A <see cref="TimeSpan"/> how long to try before erroring.</param>
        public SingleGlobalInstance(TimeSpan timeOut)
            : this(timeOut, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleGlobalInstance"/> class.
        /// </summary>
        /// <param name="timeOut">A <see cref="TimeSpan"/> how long to try before erroring.</param>
        /// <param name="id">A guid to monitor.</param>
        public SingleGlobalInstance(TimeSpan timeOut, string id)
        {
            this.timeOut = timeOut;
            mutex = InitMutex(id);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (mutex != null)
            {
                if (hasHandle)
                {
                    mutex.ReleaseMutex();
                }

                mutex.Close();
            }
        }

        /// <summary>
        /// Get the global mutex.
        /// </summary>
        /// <returns><see langword="true"/> when mutex is gotten, otherwise <see langword="false"/>.</returns>
        public bool GetMutex()
        {
            try
            {
                hasHandle = mutex.WaitOne(timeOut, exitContext: false);
            }
            catch (AbandonedMutexException)
            {
                hasHandle = true;
            }

            return hasHandle;
        }

        private static string GetApplicationGuid()
        {
            var guidAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), inherit: false);
            var guidAttribute = guidAttributes.FirstOrDefault()
                ?? throw new InvalidOperationException($"{nameof(GuidAttribute)} is not defined for this app");
            return ((GuidAttribute)guidAttribute).Value;
        }

        private static Mutex InitMutex(string id)
        {
            var mutexName = string.IsNullOrWhiteSpace(id) ? GetApplicationGuid() : id;
            var mutexId = string.Format(System.Globalization.CultureInfo.InvariantCulture, @"Global\{{{0}}}", mutexName);
            var newMutex = new Mutex(initiallyOwned: true, mutexId);

            var allowEveryoneRule = new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, domainSid: null), MutexRights.FullControl, AccessControlType.Allow);
            var securitySettings = new MutexSecurity();
            securitySettings.AddAccessRule(allowEveryoneRule);
            newMutex.SetAccessControl(securitySettings);

            return newMutex;
        }
    }
}
