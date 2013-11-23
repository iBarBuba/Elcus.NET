namespace Elcus.NET.Classess.Win32
{
    public enum IOCTL
    {
        FileDeviceUnknown = 0x00000022,
        MethodBuffered = 0,
        MethodInDirect = 1,
        MethodOutDirect = 2,
        MethodNeither = 3,

        FileAnyAccess = 0,
        FileReadAccess = 1,
        FileWriteAccess = 2
    }
}