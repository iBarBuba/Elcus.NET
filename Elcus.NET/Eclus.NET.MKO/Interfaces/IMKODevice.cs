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
        /// Возвращает максимальное число адресуемых страниц в памяти ОУ
        /// </summary>
        /// <returns></returns>
        ushort rtgetmaxpage();
        /// <summary>
        /// Выбрать страницу в памяти ОУ, с которой будем работать
        /// </summary>
        /// <param name="rtPage"></param>
        /// <exception cref="MKODeviceException"></exception>
        void rtdefpage(ushort rtPage);
        /// <summary>
        /// Выбрать подадрес в выбранной странице в ДОЗУ ОУ
        /// </summary>
        /// <param name="regime">Режим работы подадреса (прием/передача данных)</param>
        /// <param name="rtSubAddr">Подадрес</param>
        void rtdefsubaddr(RTRegime regime, ushort rtSubAddr);
        /// <summary>
        /// Записать блок данных <paramref name="data"/> по адресу <paramref name="rtAddr"/> в выбарнный подадрес в выбранной странице ДОЗУ ОУ
        /// </summary>
        /// <param name="rtAddr">Адрес</param>
        /// <param name="data">Данные</param>
        void rtputblk(ushort rtAddr, ushort[] data);
        /// <summary>
        /// Считать блок данных по адресу <paramref name="rtAddr"/> из выбранного подадреса в выбранной странице ДОЗУ ОУ
        /// </summary>
        /// <param name="rtAddr">Адрес</param>
        /// <param name="data">Блок данных, куда поместить ответ</param>
        /// <returns></returns>
        void rtgetblk(ushort rtAddr, ref ushort[] data);
        /// <summary>
        /// Записать слово данных <paramref name="rtData"/> по адресу <paramref name="rtAddr"/>
        /// </summary>
        /// <param name="rtAddr">Адрес в памяти ОУ</param>
        /// <param name="rtData">Слово данных</param>
        void rtputw(ushort rtAddr, ushort rtData);
        /// <summary>
        /// Получить слово данных, находящееся в памяти ОУ по адресу <paramref name="rtAddr"/>
        /// </summary>
        /// <param name="rtAddr">Адрес в памяти ОУ</param>
        /// <returns></returns>
        ushort rtgetw(ushort rtAddr);
        /// <summary>
        /// Включить режим монитора
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        void mtreset();
    }
}