using System.IO;
using System.Runtime.InteropServices;
using Eclus.NET.MKO.Enums;
using Eclus.NET.MKO.Exceptions;
using Eclus.NET.MKO.Interfaces;
using System;

namespace Eclus.NET.MKO
{
    internal class USBDevice : IMKODevice
    {
        #region Private fields



        private readonly int m_TmkMaxNum;
        private readonly int m_DeviceNum;
        private IntPtr m_DeviceHandle;
        private IntPtr m_DeviceEvent;
        private ushort[] m_InBuf = new ushort[4];
        private ushort[] m_OutBuf = new ushort[6];
        private int m_Result;



        #endregion
        #region Private static methods



        private static int CTL_CODE(int code)
        {
            return Win32.CTL_CODE(IOCTL.FILE_DEVICE_UNKNOWN, code, IOCTL.METHOD_BUFFERED, IOCTL.METHOD_BUFFERED);
        }

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int Read_DLL_EvD_usb(IntPtr ptr);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int tmkconfig_usb(int tmknumber, int wType, int PortAddress1, int PortAddress2);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int tmkdone_usb(int tmknumber);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int tmkselect_usb(int tmknumber);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort tmkgethwver_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tmkdefevent_usb(IntPtr hEvent);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tmkclrcwbits_usb(ushort tmkClrCWBits);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort tmkgetcwbits_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort tmkgetmode_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void tmksetcwbits_usb(ushort tmkSetCWBits);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcdefbase_usb(ushort wBase);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcdefbus_usb(ushort bcBus);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcdefirqmode_usb(ushort mode);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcdeflink_usb(ushort wBase, ushort bcCtrlCodeX);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint bcgetansw_usb(ushort bcCtrlCode);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcgetbase_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void bcgetblk_usb(ushort wAddr, IntPtr data_ptr, int length);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcgetbus_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcgetirqmode_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint bcgetlink_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcgetmaxbase_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint bcgetstate_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcgetw_usb(ushort wAddr);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void bcputblk_usb(ushort wAddr, IntPtr data_ptr, int length);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void bcputw_usb(ushort wAddr, ushort data);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcreset_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcstart_usb(ushort bcBase, ushort bcCtrlCode);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcstartx_usb(ushort bcBase, ushort bcCtrlCodeX);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort bcstop_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtbusy_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtclranswbits_usb(ushort rtClrBits);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtclrflag_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtdefaddress_usb(ushort rtAddr);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtdefirqmode_usb(ushort mode);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetirqmode_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtdefmode_usb(ushort mode);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtdefpage_usb(ushort rtPage);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtdefpagebus_usb(ushort rtPageBus);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtdefpagepc_usb(ushort rtPagePC);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtdefsubaddr_usb(ushort regime, ushort rtSubAddr);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtenable_usb(ushort rtEnable);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetaddress_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetanswbits_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtgetblk_usb(ushort rtAddr, IntPtr data_ptr, ushort length);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetcmddata_usb(ushort rtBusCommand);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetflag_usb(ushort rtDir, ushort rtSubAddr);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetmaxpage_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetmode_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetpage_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetpagebus_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetpagepc_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetstate_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetsubaddr_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtgetw_usb(ushort rtAddr);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtlock_usb(ushort rtDir, ushort rtSubAddr);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtputblk_usb(ushort rtAddr, IntPtr data_ptr, ushort length);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtputcmddata_usb(ushort rtBusCommand, ushort rtData);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtputw_usb(ushort rtAddr, ushort rtData);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort rtreset_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtsetanswbits_usb(ushort rtSetBits);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtsetflag_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void rtunlock_usb();
        
        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort mtreset_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort mtdefbase_usb(ushort mtBasePC);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort MonitorHwStart_usb(ushort dwBufSize, IntPtr hEvent);

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort MonitorHwStop_usb();

        [DllImport("USB_TA_DRV.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern ushort MonitorHwGetMessage_usb(IntPtr Data, ushort dwBufSize, short FillFlag, IntPtr dwMsWritten);



        #endregion
        #region Public constructors



        public USBDevice(int deviceNum, int tmkMaxNum)
        {
            m_DeviceNum = deviceNum;
            m_TmkMaxNum = tmkMaxNum;
            tmkconfig();
            tmkselect();
        }



        #endregion
        #region Implementation of IMKODevice



        public int tmkgetmaxn()
        {
            return m_TmkMaxNum;
        }

        /// <summary>
        /// Подключиться к устройству
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        public void tmkconfig()
        {
            if (m_DeviceHandle != IntPtr.Zero)
                return;

            var filename = string.Format("\\\\.\\EZUSB-{0}", m_DeviceNum);
            m_DeviceHandle = Win32.CreateFile(filename, FileAccess.ReadWrite, FileShare.None, IntPtr.Zero, FileMode.Open,
                128, IntPtr.Zero);

            if (m_DeviceHandle.ToInt32() == -1)
                throw new MKODeviceException(ErrorType.TMK_BAD_NUMBER);

            if (Win32.DeviceIoControl(m_DeviceHandle, CTL_CODE(2082), m_InBuf, m_InBuf.Length, m_OutBuf, m_OutBuf.Length,
                out m_Result, null))
            {
                var code = (ErrorType)tmkconfig_usb(m_DeviceNum, 9, 0, 0);
                if (code != ErrorType.TMK_SUCCESSFULL)
                    throw new MKODeviceException(code);
            }
            else
            {
                Win32.CloseHandle(m_DeviceHandle);
                m_DeviceHandle = IntPtr.Zero;
                throw new MKODeviceException(ErrorType.TMK_BAD_NUMBER);
            }
        }

        /// <summary>
        /// Подключиться к устройству
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        public void tmkselect()
        {
            var code = (ErrorType)tmkselect_usb(m_DeviceNum);
            if (code != ErrorType.TMK_SUCCESSFULL)
                throw new MKODeviceException(code);
        }

        public void tmkdone()
        {
            if (m_DeviceHandle != IntPtr.Zero)
                Win32.CloseHandle(m_DeviceHandle);
            m_DeviceHandle = IntPtr.Zero;
            m_DeviceEvent = IntPtr.Zero;
            tmkdone_usb(m_DeviceNum);
        }

        /// <summary>
        /// Получить режим работы устройства
        /// </summary>
        /// <returns></returns>
        public Mode tmkgetmode()
        {
            return (Mode)tmkgetmode_usb();
        }

        /// <summary>
        /// Включить режим контроллера канала
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        public void bcreset()
        {
            if ((ErrorType)bcreset_usb() != ErrorType.TMK_SUCCESSFULL)
                throw new MKODeviceException(ErrorType.TMK_BAD_FUNC, @"Устройство не поддерживает работу в режиме КК");
        }

        /// <summary>
        /// Включить режим оконечного устройства
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        public void rtreset()
        {
            if ((ErrorType)rtreset_usb() != ErrorType.TMK_SUCCESSFULL)
                throw new MKODeviceException(ErrorType.TMK_BAD_FUNC, @"Устройство не поддерживает работу в режиме ОУ");
        }

        /// <summary>
        /// Возвращает максимальное число адресуемых страниц в памяти ОУ
        /// </summary>
        /// <returns></returns>
        public ushort rtgetmaxpage()
        {
            return rtgetmaxpage_usb();
        }

        /// <summary>
        /// Выбрать страницу в памяти ОУ, с которой будем работать
        /// </summary>
        /// <param name="rtPage"></param>
        /// <exception cref="MKODeviceException"></exception>
        public void rtdefpage(ushort rtPage)
        {
            if ((ErrorType)rtdefpage_usb(rtPage) != ErrorType.TMK_SUCCESSFULL)
                throw new MKODeviceException(ErrorType.RT_BAD_PAGE, @"Задан недопустимый номер в ДОЗУ");
        }

        /// <summary>
        /// Выбрать подадрес в выбранной странице в ДОЗУ ОУ
        /// </summary>
        /// <param name="regime">Режим работы подадреса (прием/передача данных)</param>
        /// <param name="rtSubAddr">Подадрес</param>
        public void rtdefsubaddr(RTRegime regime, ushort rtSubAddr)
        {
            rtdefsubaddr_usb((ushort)regime, rtSubAddr);
        }

        /// <summary>
        /// Записать блок данных <paramref name="data"/> по адресу <paramref name="rtAddr"/> в выбарнный подадрес в выбранной странице ДОЗУ ОУ
        /// </summary>
        /// <param name="rtAddr">Адрес</param>
        /// <param name="data">Данные</param>
        public void rtputblk(ushort rtAddr, ushort[] data)
        {
            var length = data.Length > 32 ? 32 : data.Length;
            var source = new short[length];
            for (var index = 0; index < length; index++)
                source[index] = BitConverter.ToInt16(BitConverter.GetBytes(data[index]), 0);
            var ptr = Marshal.AllocHGlobal(2 * length);
            Marshal.Copy(source, 0, ptr, length);

            rtputblk_usb(rtAddr, ptr, (ushort)length);
        }

        /// <summary>
        /// Считать блок данных по адресу <paramref name="rtAddr"/> из выбранного подадреса в выбранной странице ДОЗУ ОУ
        /// </summary>
        /// <param name="rtAddr">Адрес</param>
        /// <param name="data">Блок данных, куда поместить ответ</param>
        /// <returns></returns>
        public void rtgetblk(ushort rtAddr, ref ushort[] data)
        {
            var length = data.Length > 32 ? 32 : data.Length;
            var destination = new short[length];
            var ptr = Marshal.AllocHGlobal(2 * length);

            rtgetblk_usb(rtAddr, ptr, (ushort)length);

            Marshal.Copy(ptr, destination, 0, length);
            Marshal.FreeHGlobal(ptr);
            for (var index = 0; index < length; index++)
                data[index] = BitConverter.ToUInt16(BitConverter.GetBytes(destination[index]), 0);
        }

        /// <summary>
        /// Записать слово данных <paramref name="rtData"/> по адресу <paramref name="rtAddr"/>
        /// </summary>
        /// <param name="rtAddr">Адрес в памяти ОУ</param>
        /// <param name="rtData">Слово данных</param>
        public void rtputw(ushort rtAddr, ushort rtData)
        {
            rtputw_usb(rtAddr, rtData);
        }

        /// <summary>
        /// Получить слово данных, находящееся в памяти ОУ по адресу <paramref name="rtAddr"/>
        /// </summary>
        /// <param name="rtAddr">Адрес в памяти ОУ</param>
        /// <returns></returns>
        public ushort rtgetw(ushort rtAddr)
        {
            return rtgetw_usb(rtAddr);
        }

        /// <summary>
        /// Проверяет занятось выбранного ранее подадреса. Возвращает True - если занят обменом, False - адрес свободен
        /// </summary>
        /// <returns></returns>
        public bool rtbusy()
        {
            return rtbusy_usb() == 1;
        }

        /// <summary>
        /// Сбрасывает флаг во флаговом слове текущего подадреса выбранного ОУ в режиме работы со флагами
        /// </summary>
        public void rtclrflag()
        {
            rtclrflag_usb();
        }

        /// <summary>
        /// Функция программирует адрес выбранного ОУ в МК. Если тип устройства выбранного ОУ не поддерживает программирование адреса (адрес установлен перемычками на устройстве),
        /// возникает ошибочная ситуация
        /// </summary>
        /// <param name="rtAddr">Адрес в ОУ</param>
        /// <exception cref="MKODeviceException"></exception>
        public void rtdefaddress(ushort rtAddr)
        {
            switch ((ErrorType)rtdefaddress_usb(rtAddr))
            {
                case ErrorType.RT_BAD_FUNC:
                    throw new MKODeviceException(ErrorType.RT_BAD_FUNC,
                        @"Выбранное устройство не поддерживает программное задание адреса ОУ в МК");
                case ErrorType.RT_BAD_ADDRESS:
                    throw new MKODeviceException(ErrorType.RT_BAD_ADDRESS, @"Указан недопустимы адрес в ОУ");
            }
        }

        /// <summary>
        /// Установка режимов прерывания ОУ
        /// </summary>
        /// <param name="mode">Режим прерывания ОУ</param>
        public void rtdefirqmode(ushort mode)
        {
            rtdefirqmode_usb(mode);
        }

        /// <summary>
        /// Текущее значение режимов прерывания ОУ
        /// </summary>
        /// <returns></returns>
        public ushort rtgetirqmode()
        {
            return rtgetirqmode_usb();
        }

        /// <summary>
        /// Программирует режим работы выбранного ОУ
        /// </summary>
        /// <param name="mode"></param>
        /// <exception cref="MKODeviceException"></exception>
        public void rtdefmode(ushort mode)
        {
            if ((ErrorType)rtdefmode_usb(mode) != ErrorType.TMK_SUCCESSFULL)
                throw new MKODeviceException(ErrorType.RT_BAD_FUNC,
                    @"Выбранное ОУ не поддерживает программное задание режимов работы");
        }

        /// <summary>
        /// Получает режим работы ОУ
        /// </summary>
        /// <returns></returns>
        public ushort rtgetmode()
        {
            return rtgetmode_usb();
        }

        /// <summary>
        /// Номер текущей страницу в ДОЗУ ОУ
        /// </summary>
        /// <returns></returns>
        public ushort rtgetpage()
        {
            return rtgetpage_usb();
        }

        /// <summary>
        /// Номер текущего подадреса с битом "приём/передача"
        /// </summary>
        /// <returns></returns>
        public ushort rtgetsubaddr()
        {
            return rtgetsubaddr_usb();
        }

        /// <summary>
        /// Настраивает выбранное ОУ и драйвер на дальнейшую работу с ДОЗУ в указанном подадресе и при этом блокирует подадрес для доступа со стороны МК. Если в момент
        /// вызова идёт обмен данными с МК, то выполнение откладывается до окончания обмена. Статус блокировки можно проверить с помощью rtbusy()
        /// </summary>
        /// <param name="regime"></param>
        /// <param name="rtSubAddr"></param>
        public void rtlock(RTRegime regime, ushort rtSubAddr)
        {
            rtlock_usb((ushort)regime, rtSubAddr);
        }

        /// <summary>
        /// Разблокирует ранее заблокированный вызовом rtlock() подадрес
        /// </summary>
        public void rtunlock()
        {
            rtunlock_usb();
        }

        /// <summary>
        /// Включить/выбранное устройство ОУ, не выключая сам режим ОУ
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        /// <exception cref="MKODeviceException"></exception>
        public RTEnableMode rtenable(RTEnableMode mode)
        {
            if (mode == RTEnableMode.RT_GET_ENABLE)
                return (RTEnableMode)rtenable_usb((ushort)mode);

            var result = rtenable_usb((ushort)mode);
            if ((ErrorType)result != ErrorType.TMK_SUCCESSFULL)
                throw new MKODeviceException(ErrorType.RT_BAD_FUNC, @"Ошибка задания параметра");

            return (RTEnableMode)rtenable_usb((ushort)RTEnableMode.RT_GET_ENABLE);
        }

        /// <summary>
        /// Включить режим монитора
        /// </summary>
        /// <exception cref="MKODeviceException"></exception>
        public void mtreset()
        {
            if ((ErrorType)mtreset_usb() != ErrorType.TMK_SUCCESSFULL)
                throw new MKODeviceException(ErrorType.TMK_BAD_FUNC, @"Устройство не поддерживает работу в режиме МТ");
        }



        #endregion
        #region Implementation of IDisposable



        public void Dispose()
        {
            tmkdone();
        }



        #endregion
    }
}
