using System.IO;
using System.Runtime.InteropServices;
using Eclus.NET.MKO.Enums;
using Eclus.NET.MKO.Exceptions;
using Eclus.NET.MKO.Interfaces;
using System;

namespace Eclus.NET.MKO
{
    internal class USBDevice : IMKODevice
    {
        #region Private fields



        private readonly int m_TmkMaxNum;
        private readonly int m_DeviceNum;
        private IntPtr m_DeviceHandle;
        private IntPtr m_DeviceEvent;
        private readonly ushort[] m_InBuf = new ushort[4];
        private readonly ushort[] m_OutBuf = new ushort[6];
        private int m_Result;



        #endregion
        #region Private static methods



        private static int CTL_CODE(int code)
        {
            return Win32.CTL_CODE(IOCTL.FILE_DEVICE_UNKNOWN, code, IOCTL.METHOD_BUFFERED, IOCTL.METHOD_BUFFERED);
        }

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int tmkconfig_usb(int tmknumber, int wType, int PortAddress1, int PortAddress2);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int tmkdone_usb(int tmknumber);



        #endregion
        #region Public constructors



        public USBDevice(int deviceNum, int tmkMaxNum)
        {
            m_DeviceNum = deviceNum;
            m_TmkMaxNum = tmkMaxNum;
            tmkconfig();
        }



        #endregion
        #region Implementation of IMKODevice



        public int tmkgetmaxn()
        {
            return m_TmkMaxNum;
        }

        /// <summary>
        /// Подключиться к устройству
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        public void tmkconfig()
        {
            if (m_DeviceHandle != IntPtr.Zero)
                return;

            var filename = string.Format("\\\\.\\EZUSB-{0}", m_DeviceNum);
            m_DeviceHandle = Win32.CreateFile(filename, FileAccess.ReadWrite, FileShare.None, IntPtr.Zero, FileMode.Open,
                128, IntPtr.Zero);

            if (m_DeviceHandle.ToInt32() == -1)
                throw new MKODeviceException(ErrorType.TMK_BAD_NUMBER);

            if (Win32.DeviceIoControl(m_DeviceHandle, CTL_CODE(2082), m_InBuf, m_InBuf.Length, m_OutBuf, m_OutBuf.Length,
                out m_Result, null))
            {
                var code = (ErrorType)tmkconfig_usb(m_DeviceNum, 9, 0, 0);
                if (code != ErrorType.TMK_SUCCESSFULL)
                    throw new MKODeviceException(code);
            }
            else
            {
                Win32.CloseHandle(m_DeviceHandle);
                m_DeviceHandle = IntPtr.Zero;
                throw new MKODeviceException(ErrorType.TMK_BAD_NUMBER);
            }
        }

        public void tmkdone()
        {
            if (m_DeviceHandle != IntPtr.Zero)
                Win32.CloseHandle(m_DeviceHandle);
            m_DeviceHandle = IntPtr.Zero;
            m_DeviceEvent = IntPtr.Zero;
            tmkdone_usb(m_DeviceNum);
        }



        #endregion
        #region Implementation of IDisposable



        public void Dispose()
        {
            tmkdone();
        }



        #endregion
    }
}
