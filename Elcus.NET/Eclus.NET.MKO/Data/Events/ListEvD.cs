using System.Runtime.InteropServices;

namespace Eclus.NET.MKO.Data.Events
{
    [StructLayout(LayoutKind.Sequential)]
    public class ListEvD
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)] public uint[] awEvData = new uint[3];
        public int nInt;
        public uint wMode;
        public int empty1;
        public int empty2;
    }
}