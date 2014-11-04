using System.Runtime.InteropServices;

namespace Eclus.NET.MKO.Data.Events
{
    [StructLayout(LayoutKind.Explicit)]
    public struct TmkEventData
    {
        [FieldOffset(0)]
        public int Int;
        [FieldOffset(4)]
        public ushort Mode;
        [FieldOffset(6)]
        public BC bc;
        [FieldOffset(6)]
        public BCx bcx;
        [FieldOffset(6)]
        public RT rt;
        [FieldOffset(6)]
        public MT mt;
        [FieldOffset(6)]
        public MRT mrt;
    }
}