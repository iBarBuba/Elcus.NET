using System;
using Eclus.NET.MKO.Data.Events;
using Eclus.NET.MKO.Enums;
using Eclus.NET.MKO.Exceptions;
using Eclus.NET.MKO.Interfaces;

namespace Eclus.NET.MKO
{
    internal class PCIDevice : GeneralDevice, IMKODevice
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
        /// Получить данные по состоявшемуся прерыванию в МКО
        /// </summary>
        /// <param name="evd"></param>
        public void tmkgetevd(ref TmkEventData evd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Проинициализировать обработчики событий
        /// </summary>
        public void initevents()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ожидание наступления события от МКО
        /// </summary>
        /// <param name="milliseconds">Время, мс, ожидания наступления события</param>
        public MKOEvents WaitForEvents(uint milliseconds)
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
        /// Проверяет занятось выбранного ранее подадреса. Возвращает True - если занят обменом, False - адрес свободен
        /// </summary>
        /// <returns></returns>
        public bool rtbusy()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Сбрасывает флаг во флаговом слове текущего подадреса выбранного ОУ в режиме работы со флагами
        /// </summary>
        public void rtclrflag()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Функция программирует адрес выбранного ОУ в МК. Если тип устройства выбранного ОУ не поддерживает программирование адреса (адрес установлен перемычками на устройстве),
        /// возникает ошибочная ситуация
        /// </summary>
        /// <param name="rtAddr">Адрес в ОУ</param>
        /// <exception cref="MKODeviceException"></exception>
        public void rtdefaddress(ushort rtAddr)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Получить адрес ОУ в МК
        /// </summary>
        /// <returns></returns>
        public ushort rtgetaddress()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Установка режимов прерывания ОУ
        /// </summary>
        /// <param name="mode">Режим прерывания ОУ</param>
        public void rtdefirqmode(ushort mode)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Текущее значение режимов прерывания ОУ
        /// </summary>
        /// <returns></returns>
        public ushort rtgetirqmode()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Программирует режим работы выбранного ОУ
        /// </summary>
        /// <param name="mode"></param>
        /// <exception cref="MKODeviceException"></exception>
        public void rtdefmode(ushort mode)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Получает режим работы ОУ
        /// </summary>
        /// <returns></returns>
        public ushort rtgetmode()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Номер текущей страницу в ДОЗУ ОУ
        /// </summary>
        /// <returns></returns>
        public ushort rtgetpage()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Номер текущего подадреса с битом "приём/передача"
        /// </summary>
        /// <returns></returns>
        public ushort rtgetsubaddr()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Настраивает выбранное ОУ и драйвер на дальнейшую работу с ДОЗУ в указанном подадресе и при этом блокирует подадрес для доступа со стороны МК. Если в момент
        /// вызова идёт обмен данными с МК, то выполнение откладывается до окончания обмена. Статус блокировки можно проверить с помощью rtbusy()
        /// </summary>
        /// <param name="regime"></param>
        /// <param name="rtSubAddr"></param>
        public void rtlock(RTRegime regime, ushort rtSubAddr)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Разблокирует ранее заблокированный вызовом rtlock() подадрес
        /// </summary>
        public void rtunlock()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Включить/выбранное устройство ОУ, не выключая сам режим ОУ
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public RTEnableMode rtenable(RTEnableMode mode)
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