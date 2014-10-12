namespace Eclus.NET.MKO.Enums
{
    /// <summary>
    /// Режим работы оконечного устройства
    /// </summary>
    public enum RTRegime : ushort
    {
        /// <summary>
        /// Приём данных
        /// </summary>
        Receive = 0,
        /// <summary>
        /// Передача данных
        /// </summary>
        Transmit = (ushort)1024
    }
}