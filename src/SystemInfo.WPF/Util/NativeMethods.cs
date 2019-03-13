using System;
using System.Runtime.InteropServices;

namespace SystemInfo.WPF.Util
{
    internal static class NativeMethods
    {
        public enum WindowLongFlags
        {
            GWL_WNDPROC = (-4),
            GWL_HINSTANCE = (-6),
            GWL_HWNDPARENT = (-8),
            GWL_STYLE = (-16),
            GWL_EXSTYLE = (-20),
            GWL_USERDATA = (-21),
            GWL_ID = (-12)
        }

        // This helper static method is required because the 32-bit version of user32.dll does not contain this API
        // (on any versions of Windows), so linking the method will fail at run-time. The bridge dispatches the request
        // to the correct function (GetWindowLong in 32-bit mode and GetWindowLongPtr in 64-bit mode)
        public static IntPtr GetWindowLongPtr(IntPtr hWnd, WindowLongFlags nIndex)
        {
            if (IntPtr.Size == 8)
            {
                return GetWindowLongPtr64(hWnd, nIndex);
            }
            else
            {
                return GetWindowLongPtr32(hWnd, nIndex);
            }
        }

        // This helper static method is required because the 32-bit version of user32.dll does not contain this API
        // (on any versions of Windows), so linking the method will fail at run-time. The bridge dispatches the request
        // to the correct function (SetWindowLong in 32-bit mode and SetWindowLongPtr in 64-bit mode)
        public static IntPtr SetWindowLongPtr(IntPtr hWnd, WindowLongFlags nIndex, IntPtr dwNewLong)
        {
            SetLastError(0);

            var result = IntPtr.Size == 8 ?
                  SetWindowLongPtr64(hWnd, nIndex, dwNewLong) :
                  new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));

            var error = Marshal.GetLastWin32Error();

            if (result == IntPtr.Zero
                && error != 0)
            {
                throw new System.ComponentModel.Win32Exception(error);
            }

            return result;
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, WindowLongFlags nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, WindowLongFlags nIndex);

        [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
        private static extern void SetLastError(int dwErrorCode);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern int SetWindowLong32(IntPtr hWnd, WindowLongFlags nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, WindowLongFlags nIndex, IntPtr dwNewLong);
    }
}