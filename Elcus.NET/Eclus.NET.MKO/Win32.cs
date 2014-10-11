using System;
using System.IO;
using System.Runtime.InteropServices;
using Eclus.NET.MKO.Enums;

namespace Eclus.NET.MKO
{
    internal class Win32
    {
        #region Internal static methods




        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateFile(
            string FileName,
            [MarshalAs(UnmanagedType.U4)] FileAccess FileAccess,
            [MarshalAs(UnmanagedType.U4)] FileShare FileShare,
            IntPtr SecurityAttribute,
            [MarshalAs(UnmanagedType.U4)] FileMode CreationDisposition,
            int Flags,
            IntPtr Template
            );

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(IntPtr Handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool DeviceIoControl(
            [In] IntPtr DeviceHandle,
            [In] int dwIOControlCode,
            [MarshalAs(UnmanagedType.LPArray), In] ushort[] lpInBuffer,
            [In] int nInBufferSize,
            [MarshalAs(UnmanagedType.LPArray), Out] ushort[] lpOutBuffer,
            int nOutBufferSuze,
            out int lpBytesReturned,
            [MarshalAs(UnmanagedType.LPArray), In] ushort[] lpOverlapped);

        internal static int CTL_CODE(int Code)
        {
            return CTL_CODE(IOCTL.FILE_DEVICE_UNKNOWN, Code, IOCTL.METHOD_BUFFERED, IOCTL.FILE_ANY_ACCESS);
        }

        internal static int CTL_CODE(IOCTL DeviceType, int Function, IOCTL Method, IOCTL FileAccess)
        {
            return ((((int)DeviceType) << 16) | ((int)FileAccess << 14) | ((Function) << 2) | (int)Method);
        }

        #endregion
    }
}