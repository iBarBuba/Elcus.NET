using System;
using Eclus.NET.MKO.Enums;
using Eclus.NET.MKO.Exceptions;

namespace Eclus.NET.MKO.Interfaces
{
    public interface IMKODevice : IDisposable
    {
        int tmkgetmaxn();
        /// <summary>
        /// Сконфигурировать устройство
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        void tmkconfig();
        /// <summary>
        /// Подключиться к устройству
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        void tmkselect();
        /// <summary>
        /// Завершить работу с устройством
        /// </summary>
        void tmkdone();
        /// <summary>
        /// Получить режим работы устройства
        /// </summary>
        /// <returns></returns>
        Mode tmkgetmode();
        /// <summary>
        /// Включить режим контроллера канала
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        void bcreset();
        /// <summary>
        /// Включить режим оконечного устройства
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        void rtreset();
        /// <summary>
        /// Включить режим монитора
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        void mtreset();
    }
}