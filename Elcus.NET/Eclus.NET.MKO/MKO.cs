using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Eclus.NET.MKO.Enums;
using Eclus.NET.MKO.Exceptions;
using Eclus.NET.MKO.Interfaces;

namespace Eclus.NET.MKO
{
    public sealed class MKO: IDisposable
    {
        #region Private fields



        private bool m_HasPCIDevices = false;
        private readonly bool[] m_USBFlag = new bool[16];
        private readonly int[] m_DeviceNumber = new int[16];
        private readonly IntPtr[] m_DeviceHandle = new IntPtr[16];
        private readonly IntPtr[] m_DeviceEvent = new IntPtr[16];
        private readonly ushort[] m_InBuf = new ushort[4];
        private readonly ushort[] m_OutBuf = new ushort[6];
        private int m_Result;



        #endregion
        #region Private methods




        private void _CheckPCIDevices(int num)
        {
            var fileName = string.Format("\\\\.\\TMK1553BDevice{0}", num);
            m_DeviceHandle[num] = Win32.CreateFile(fileName, FileAccess.ReadWrite, FileShare.None, IntPtr.Zero,
                FileMode.Open, 128, IntPtr.Zero);
            if (m_DeviceHandle[num].ToInt32() == -1)
                return;

            if (!Win32.DeviceIoControl(m_DeviceHandle[num], Win32.CTL_CODE(IOCTL.TMK_KRNLDRVR, 2134, IOCTL.METHOD_NEITHER, IOCTL.METHOD_BUFFERED), m_InBuf, 0, m_OutBuf, 2, out m_Result, null) || m_OutBuf[0] < 1024)
            {
                Win32.CloseHandle(m_DeviceHandle[num]);
                m_DeviceHandle[num] = IntPtr.Zero;
                throw new MKODeviceException(ErrorType.VTMK_BAD_VERSION, "Несовместимая версия драйвера или ошибка ОС");
            }

            m_DeviceNumber[num] = num;
            m_DeviceHandle[num] = IntPtr.Zero;
            m_HasPCIDevices = true;
        }

        private void _CheckUSBDevices(int num)
        {
            var fileName = string.Format("\\\\.\\EZUSB-{0}", num - (!m_HasPCIDevices ? 8 : 7));
            m_DeviceHandle[num] = Win32.CreateFile(fileName, FileAccess.ReadWrite, FileShare.None, IntPtr.Zero,
                FileMode.Open, 128, IntPtr.Zero);
            if (m_DeviceHandle[num].ToInt32() == -1)
                return;

            m_USBFlag[num] = true;
            m_DeviceNumber[num] = num;
            Win32.CloseHandle(m_DeviceHandle[num]);
            m_DeviceHandle[num] = IntPtr.Zero;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        private void _TmkOpen()
        {
            Enumerable.Range(0, m_USBFlag.Length).ForEach(num =>
            {
                m_USBFlag[num] = false;
                if (num < 8)
                    _CheckPCIDevices(num);
                else
                    _CheckUSBDevices(num);
            });
        }



        #endregion
        #region Public constructors



        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        public MKO()
        {
            _TmkOpen();
        }



        #endregion
        #region Indexers



        public IMKODevice this[int num]
        {
            get
            {
                if (m_DeviceHandle[num] != IntPtr.Zero)
                    throw new MKODeviceException(ErrorType.TMK_BAD_NUMBER, @"Invalid device number");

                return m_USBFlag[num]
                    ? (new USBDevice(num - (!m_HasPCIDevices ? 8 : 7), m_DeviceHandle.Count(i => i == IntPtr.Zero)) as
                        IMKODevice)
                    : new PCIDevice(num, m_DeviceHandle.Count(i => i == IntPtr.Zero));
            }
        }



        #endregion
        #region Public methods



        public IEnumerable<int> GetPossibleDeviceNumbers()
        {
            return Enumerable.Range(0, m_DeviceHandle.Length)
                .Where(n => m_DeviceHandle[n] == IntPtr.Zero)
                .Select(n => n);
        }




        #endregion
        #region Implementation of IDisposable



        /// <summary>
        /// Führt anwendungsspezifische Aufgaben aus, die mit dem Freigeben, Zurückgeben oder Zurücksetzen von nicht verwalteten Ressourcen zusammenhängen.
        /// </summary>
        public void Dispose()
        {
            Enumerable.Range(0, m_DeviceHandle.Length)
                .Where(n => m_DeviceHandle[n].ToInt32() != -1)
                .ForEach(n => Win32.CloseHandle(m_DeviceHandle[n]));
        }



        #endregion
    }
}