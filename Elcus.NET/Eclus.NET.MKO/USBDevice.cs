﻿using System.IO;
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
        private static extern int Read_DLL_EvD_usb(IntPtr ptr);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int tmkconfig_usb(int tmknumber, int wType, int PortAddress1, int PortAddress2);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int tmkdone_usb(int tmknumber);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int tmkselect_usb(int tmknumber);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort tmkgethwver_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tmkdefevent_usb(IntPtr hEvent);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tmkclrcwbits_usb(ushort tmkClrCWBits);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort tmkgetcwbits_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort tmkgetmode_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tmksetcwbits_usb(ushort tmkSetCWBits);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcdefbase_usb(ushort wBase);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcdefbus_usb(ushort bcBus);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcdefirqmode_usb(ushort mode);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcdeflink_usb(ushort wBase, ushort bcCtrlCodeX);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint bcgetansw_usb(ushort bcCtrlCode);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcgetbase_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void bcgetblk_usb(ushort wAddr, IntPtr data_ptr, int length);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcgetbus_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcgetirqmode_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint bcgetlink_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcgetmaxbase_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint bcgetstate_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcgetw_usb(ushort wAddr);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void bcputblk_usb(ushort wAddr, IntPtr data_ptr, int length);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void bcputw_usb(ushort wAddr, ushort data);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcreset_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcstart_usb(ushort bcBase, ushort bcCtrlCode);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcstartx_usb(ushort bcBase, ushort bcCtrlCodeX);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcstop_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtbusy_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtclranswbits_usb(ushort rtClrBits);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtclrflag_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtdefaddress_usb(ushort rtAddr);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtdefirqmode_usb(ushort mode);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetirqmode_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtdefmode_usb(ushort mode);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtdefpage_usb(ushort rtPage);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtdefpagebus_usb(ushort rtPageBus);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtdefpagepc_usb(ushort rtPagePC);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtdefsubaddr_usb(ushort regime, ushort rtSubAddr);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtenable_usb(ushort rtEnable);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetaddress_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetanswbits_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtgetblk_usb(ushort rtAddr, IntPtr data_ptr, ushort length);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetcmddata_usb(ushort rtBusCommand);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetflag_usb(ushort rtDir, ushort rtSubAddr);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetmaxpage_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetmode_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetpage_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetpagebus_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetpagepc_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetstate_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetsubaddr_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetw_usb(ushort rtAddr);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtlock_usb(ushort rtDir, ushort rtSubAddr);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtputblk_usb(ushort rtAddr, IntPtr data_ptr, ushort length);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtputcmddata_usb(ushort rtBusCommand, ushort rtData);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtputw_usb(ushort rtAddr, ushort rtData);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtreset_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtsetanswbits_usb(ushort rtSetBits);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtsetflag_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtunlock_usb();
        
        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort mtreset_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort mtdefbase_usb(ushort mtBasePC);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort MonitorHwStart_usb(ushort dwBufSize, IntPtr hEvent);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort MonitorHwStop_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort MonitorHwGetMessage_usb(IntPtr Data, ushort dwBufSize, short FillFlag, IntPtr dwMsWritten);



        #endregion
        #region Public constructors



        public USBDevice(int deviceNum, int tmkMaxNum)
        {
            m_DeviceNum = deviceNum;
            m_TmkMaxNum = tmkMaxNum;
            tmkconfig();
            tmkselect();
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

        /// <summary>
        /// Подключиться к устройству
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        public void tmkselect()
        {
            var code = (ErrorType)tmkselect_usb(m_DeviceNum);
            if (code != ErrorType.TMK_SUCCESSFULL)
                throw new MKODeviceException(code);
        }

        public void tmkdone()
        {
            if (m_DeviceHandle != IntPtr.Zero)
                Win32.CloseHandle(m_DeviceHandle);
            m_DeviceHandle = IntPtr.Zero;
            m_DeviceEvent = IntPtr.Zero;
            tmkdone_usb(m_DeviceNum);
        }

        /// <summary>
        /// Получить режим работы устройства
        /// </summary>
        /// <returns></returns>
        public Mode tmkgetmode()
        {
            return (Mode)tmkgetmode_usb();
        }

        /// <summary>
        /// Включить режим контроллера канала
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        public void bcreset()
        {
            if ((ErrorType)bcreset_usb() != ErrorType.TMK_SUCCESSFULL)
                throw new MKODeviceException(ErrorType.TMK_BAD_FUNC, @"Устройство не поддерживает работу в режиме КК");
        }

        /// <summary>
        /// Включить режим оконечного устройства
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        public void rtreset()
        {
            if ((ErrorType)rtreset_usb() != ErrorType.TMK_SUCCESSFULL)
                throw new MKODeviceException(ErrorType.TMK_BAD_FUNC, @"Устройство не поддерживает работу в режиме ОУ");
        }

        /// <summary>
        /// Включить режим монитора
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        public void mtreset()
        {
            if ((ErrorType)mtreset_usb() != ErrorType.TMK_SUCCESSFULL)
                throw new MKODeviceException(ErrorType.TMK_BAD_FUNC, @"Устройство не поддерживает работу в режиме МТ");
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
