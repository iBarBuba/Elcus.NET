﻿using Eclus.NET.MKO.Enums;
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
        /// Возвращает максимальное число адресуемых страниц в памяти ОУ
        /// </summary>
        /// <returns></returns>
        public ushort rtgetmaxpage()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Выбрать страницу в памяти ОУ, с которой будем работать
        /// </summary>
        /// <param name="rtPage"></param>
        /// <exception cref="MKODeviceException"></exception>
        public void rtdefpage(ushort rtPage)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Выбрать подадрес в выбранной странице в ДОЗУ ОУ
        /// </summary>
        /// <param name="regime">Режим работы подадреса (прием/передача данных)</param>
        /// <param name="rtSubAddr">Подадрес</param>
        public void rtdefsubaddr(RTRegime regime, ushort rtSubAddr)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Записать блок данных <paramref name="data"/> по адресу <paramref name="rtAddr"/> в выбарнный подадрес в выбранной странице ДОЗУ ОУ
        /// </summary>
        /// <param name="rtAddr">Адрес</param>
        /// <param name="data">Данные</param>
        public void rtputblk(ushort rtAddr, ushort[] data)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Считать блок данных по адресу <paramref name="rtAddr"/> из выбранного подадреса в выбранной странице ДОЗУ ОУ
        /// </summary>
        /// <param name="rtAddr">Адрес</param>
        /// <param name="data">Блок данных, куда поместить ответ</param>
        /// <returns></returns>
        public void rtgetblk(ushort rtAddr, ref ushort[] data)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Записать слово данных <paramref name="rtData"/> по адресу <paramref name="rtAddr"/>
        /// </summary>
        /// <param name="rtAddr">Адрес в памяти ОУ</param>
        /// <param name="rtData">Слово данных</param>
        public void rtputw(ushort rtAddr, ushort rtData)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Получить слово данных, находящееся в памяти ОУ по адресу <paramref name="rtAddr"/>
        /// </summary>
        /// <param name="rtAddr">Адрес в памяти ОУ</param>
        /// <returns></returns>
        public ushort rtgetw(ushort rtAddr)
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