using System;
using Eclus.NET.MKO.Exceptions;

namespace Eclus.NET.MKO.Interfaces
{
    public interface IMKODevice : IDisposable
    {
        int tmkgetmaxn();
        /// <summary>
        /// Подключиться к устройству
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        void tmkconfig();
        /// <summary>
        /// Завершить работу с устройством
        /// </summary>
        void tmkdone();
    }
}