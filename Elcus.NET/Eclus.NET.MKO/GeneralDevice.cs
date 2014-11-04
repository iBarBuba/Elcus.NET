using System;

namespace Eclus.NET.MKO
{
    internal class GeneralDevice
    {
        #region Internal methods



        internal IntPtr CreateEvent()
        {
            return Win32.CreateEvent(IntPtr.Zero, true, false, "");
        }

        internal bool ResetEvent(IntPtr handle)
        {
            return Win32.ResetEvent(handle);
        }

        internal bool CloseEvent(IntPtr handle)
        {
            return Win32.CloseHandle(handle);
        }



        #endregion
    }
}