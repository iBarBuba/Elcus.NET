namespace Eclus.NET.MKO.Enums
{
    public enum CMD : ushort
    {
        CMDSynchronizeWithDataWord = (ushort)17,
        CMDDynamicBUSControl = (ushort)1024,
        CMDSynchronize = (ushort)1025,
        CMDTransmitStatusWord = (ushort)1026,
        CMDInitiateSelfTest = (ushort)1027,
        CMDTransmitterShutdown = (ushort)1028,
        CMDOverrideTransmitterShutdown = (ushort)1029,
        CMDInhibitTerminalFlagBit = (ushort)1030,
        CMDOverrideInhibitTerminalFlagBit = (ushort)1031,
        CMDResetRemoteTerminal = (ushort)1032,
        CMDTransmitVectorWord = (ushort)1040,
        CMDTransmitLastCommandWord = (ushort)1042,
        CMDTransmitBuiltInTestWord = (ushort)1043,
    }
}