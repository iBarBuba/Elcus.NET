namespace Eclus.NET.MKO.Enums
{
    public enum CtrlCode : ushort
    {
        CX_BUS_A = (ushort)0,
        CX_NOSIG = (ushort)0,
        CX_STOP = (ushort)0,
        DATA_BC_RT = (ushort)0,
        DATA_RT_BC = (ushort)1,
        DATA_RT_RT = (ushort)2,
        CTRL_C_A = (ushort)3,
        CTRL_CD_A = (ushort)4,
        CTRL_C_AD = (ushort)5,
        DATA_BC_RT_BRCST = (ushort)8,
        DATA_RT_RT_BRCST = (ushort)10,
        CTRL_C_BRCST = (ushort)11,
        CTRL_CD_BRCST = (ushort)12,
        CX_CONT = (ushort)16,
        CX_BUS_B = (ushort)32,
        CX_SIG = (ushort)32768,
    }
}