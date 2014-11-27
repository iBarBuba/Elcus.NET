using System;

namespace Eclus.NET.MKO
{
    internal class GeneralDevice : IDisposable
    {
        #region Private fields



        private bool m_IsDisposed;



        #endregion
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
        #region Protected methods




        protected virtual void Dispose(bool disposing)
        {
            m_IsDisposed = true;
        }



        #endregion
        #region Destructors



        ~GeneralDevice()
        {
            Dispose(false);
        }



        #endregion
        #region Implementation of IDisposable



        public void Dispose()
        {
            if (m_IsDisposed)
                return;

            Dispose(true);
            GC.SuppressFinalize(ToString());
        }



        #endregion
    }
}