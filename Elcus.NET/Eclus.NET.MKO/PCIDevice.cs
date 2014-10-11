using Eclus.NET.MKO.Interfaces;

namespace Eclus.NET.MKO
{
    internal class PCIDevice : IMKODevice
    {
        #region Public constructors



        public PCIDevice(int num, int maxN)
        {

        }



        #endregion
        #region Implementation of IMKODevice



        public int tmkgetmaxn()
        {
            return 0;
        }

        public void tmkconfig()
        {
            throw new System.NotImplementedException();
        }

        public void tmkdone()
        {
            throw new System.NotImplementedException();
        }



        #endregion
        #region Implementation of IDisposable



        public void Dispose()
        {
        }



        #endregion
    }
}