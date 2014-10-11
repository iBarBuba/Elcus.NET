using Eclus.NET.MKO.Interfaces;

namespace Eclus.NET.MKO
{
    public sealed class MKO
    {
        #region Public static methods



        public static IMKODevice Find()
        {
            return new USBDevice();
        }



        #endregion
    }
}