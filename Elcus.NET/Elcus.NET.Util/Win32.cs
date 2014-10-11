using System;
using System.IO;
using System.Runtime.InteropServices;
using Elcus.NET.Util.Enums;

namespace Elcus.NET.Util
{
    public class Win32
    {
        #region Internal static methods




        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFile(
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
        public static extern bool CloseHandle(IntPtr Handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool DeviceIoControl(IntPtr DeviceHandle, int IOControlMode,
            IntPtr InBuffer,
            int InBufferSize,
            IntPtr OutBuffer,
            int OutBufferSuze,
            ref int BytesReturned,
            IntPtr Overlapped);



        public static int CTL_CODE(int Code)
        {
            return CTL_CODE(IOCTL.FileDeviceUnknown, Code, IOCTL.MethodBuffered, IOCTL.FileAnyAccess);
        }

        public static int CTL_CODE(IOCTL DeviceType, int Function, IOCTL Method, IOCTL FileAccess)
        {
            return ((((int)DeviceType) << 16) | ((int)FileAccess << 14) | ((Function) << 2) | (int)Method);
        }

        #endregion
    }
}