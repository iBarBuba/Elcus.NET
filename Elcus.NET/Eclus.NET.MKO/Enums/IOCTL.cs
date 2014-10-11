namespace Eclus.NET.MKO.Enums
{
    internal enum IOCTL
    {
        FILE_ANY_ACCESS = 0,
        METHOD_BUFFERED = 0,
        FILE_READ_ACCESS = 1,
        METHOD_IN_DIRECT = 1,
        FILE_WRITE_ACCESS = 2,
        METHOD_OUT_DIRECT = 2,
        METHOD_NEITHER = 3,
        FILE_DEVICE_UNKNOWN = 34,
        Ezusb_IOCTL_INDEX = 2048,
        TMK_KRNLDRVR = 32768,
    }
}