using Eclus.NET.MKO.Enums;
using Eclus.NET.MKO.Exceptions;
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

        /// <summary>
        /// Подключиться к устройству
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        public void tmkselect()
        {
            throw new System.NotImplementedException();
        }

        public void tmkdone()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Получить режим работы устройства
        /// </summary>
        /// <returns></returns>
        public Mode tmkgetmode()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Включить режим контроллера канала
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        public void bcreset()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Включить режим оконечного устройства
        /// </summary>
        public void rtreset()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Включить режим монитора
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        public void mtreset()
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