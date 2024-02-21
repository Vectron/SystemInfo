using System.Runtime.InteropServices;

namespace SystemInfo.WPF.Util;

/// <summary>
/// Native methods.
/// </summary>
internal static partial class NativeMethods
{
    /// <summary>
    /// Represents properties that can be set or retrieved from the window.
    /// </summary>
    public enum WindowLongFlags
    {
        /// <summary>
        /// Sets or gets a new address for the window procedure.
        /// </summary>
        GWL_WNDPROC = -4,

        /// <summary>
        /// Sets or gets a new application instance handle.
        /// </summary>
        GWL_HINSTANCE = -6,

        /// <summary>
        /// Retrieves a handle to the parent window, if there is one.
        /// </summary>
        GWL_HWNDPARENT = -8,

        /// <summary>
        /// Sets or gets a new window style.
        /// </summary>
        GWL_STYLE = -16,

        /// <summary>
        /// Sets or gets a new extended window style.
        /// </summary>
        GWL_EXSTYLE = -20,

        /// <summary>
        /// Sets or gets the user data associated with the window. This data is intended for use by the application that created the window. Its value is initially zero.
        /// </summary>
        GWL_USERDATA = -21,

        /// <summary>
        /// Sets or gets a new identifier of the child window. The window cannot be a top-level window.
        /// </summary>
        GWL_ID = -12,
    }

    /// <summary>
    /// Retrieves information about the specified window. The function also retrieves the value at a specified offset into the extra window memory.
    /// </summary>
    /// <remarks>
    /// This helper static method is required because the 32-bit version of user32.dll does not contain this API
    /// (on any versions of Windows), so linking the method will fail at run-time. The bridge dispatches the request
    /// to the correct function (GetWindowLong in 32-bit mode and GetWindowLongPtr in 64-bit mode).
    /// </remarks>
    /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
    /// <param name="nIndex">The zero-based offset to the value to be retrieved.
    /// Valid values are in the range zero through the number of bytes of extra window memory, minus the size of a LONG_PTR.
    /// To retrieve any other value, specify one of the following values.</param>
    /// <returns>If the function succeeds, the return value is the requested value.</returns>
    public static IntPtr GetWindowLongPtr(IntPtr hWnd, WindowLongFlags nIndex)
    {
        if (IntPtr.Size == 8)
        {
            return GetWindowLongPtr64(hWnd, nIndex);
        }

        return GetWindowLongPtr32(hWnd, nIndex);
    }

    /// <summary>
    /// Changes an attribute of the specified window. The function also sets a value at the specified offset in the extra window memory.
    /// </summary>
    /// <remarks>
    /// This helper static method is required because the 32-bit version of user32.dll does not contain this API
    /// (on any versions of Windows), so linking the method will fail at run-time. The bridge dispatches the request
    /// to the correct function (SetWindowLong in 32-bit mode and SetWindowLongPtr in 64-bit mode).
    /// </remarks>
    /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.
    /// The SetWindowLongPtr function fails if the process that owns the window specified by the hWnd parameter is at a higher process privilege in the UIPI hierarchy than the process the calling thread resides in.
    /// Windows XP/2000:   The SetWindowLongPtr function fails if the window specified by the hWnd parameter does not belong to the same process as the calling thread.</param>
    /// <param name="nIndex">The zero-based offset to the value to be set.
    /// Valid values are in the range zero through the number of bytes of extra window memory, minus the size of a LONG_PTR. To set any other value, specify one of the following values.</param>
    /// <param name="dwNewLong">The replacement value.</param>
    /// <returns>If the function succeeds, the return value is the previous value of the specified offset.
    /// If the function fails, the return value is zero.To get extended error information, call GetLastError.
    /// If the previous value is zero and the function succeeds, the return value is zero, but the function does not clear the last error information.
    /// To determine success or failure, clear the last error information by calling SetLastError with 0, then call SetWindowLongPtr.Function failure will be indicated by a return value of zero and a GetLastError result that is nonzero.</returns>
    /// <exception cref="System.ComponentModel.Win32Exception">Thrown when the method fails.</exception>
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

    [LibraryImport("user32.dll", EntryPoint = "GetWindowLongA")]
    private static partial IntPtr GetWindowLongPtr32(IntPtr hWnd, WindowLongFlags nIndex);

    [LibraryImport("user32.dll", EntryPoint = "GetWindowLongPtrA")]
    private static partial IntPtr GetWindowLongPtr64(IntPtr hWnd, WindowLongFlags nIndex);

    [LibraryImport("kernel32.dll", EntryPoint = "SetLastError")]
    private static partial void SetLastError(int dwErrorCode);

    [LibraryImport("user32.dll", EntryPoint = "SetWindowLongA", SetLastError = true)]
    private static partial int SetWindowLong32(IntPtr hWnd, WindowLongFlags nIndex, int dwNewLong);

    [LibraryImport("user32.dll", EntryPoint = "SetWindowLongPtrA", SetLastError = true)]
    private static partial IntPtr SetWindowLongPtr64(IntPtr hWnd, WindowLongFlags nIndex, IntPtr dwNewLong);
}
