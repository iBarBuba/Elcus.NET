using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eclus.NET.MKO;
using Eclus.NET.MKO.Enums;
using Eclus.NET.MKO.Exceptions;

namespace SimpleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var mko = new MKO())
            {
                using (var device = mko[mko.GetPossibleDeviceNumbers().FirstOrDefault()])
                {
                    try
                    {
                        //режим ОУ
                        device.rtreset();
                        var wMaxPage = device.rtgetmaxpage();
                        Console.WriteLine("rtMaxPage = {0}", wMaxPage);
                        for (ushort wPage = 0; wPage <= wMaxPage; wPage++)
                        {
                            device.rtdefpage(wPage);
                            for (ushort wSubAddr = 0; wSubAddr <= 0x1F; wSubAddr++)
                            {
                                device.rtdefsubaddr(RTRegime.Receive, wSubAddr);
                                for (ushort wAddr = 0; wAddr <= 31; wAddr++)
                                    device.rtputw(wAddr, (ushort)(wAddr | (wSubAddr << 8) | (wPage << 13)));
                                device.rtdefsubaddr(RTRegime.Transmit, wSubAddr);
                                for (ushort wAddr = 0; wAddr <= 31; wAddr++)
                                    device.rtputw(wAddr, (ushort)((wAddr + 32) | (wSubAddr << 8) | (wPage << 13)));
                            }
                        }

                        var withoutErrors = true;
                        for (ushort wPage = 0; wPage <= wMaxPage; wPage++)
                        {
                            device.rtdefpage(wPage);
                            for (ushort wSubAddr = 0; wSubAddr <= 31; wSubAddr++)
                            {
                                device.rtdefsubaddr(RTRegime.Receive, wSubAddr);
                                for (ushort wAddr = 0; wAddr <= 31; wAddr++)
                                    withoutErrors &= device.rtgetw(wAddr) ==
                                                 (ushort)((wAddr) | (wSubAddr << 8) | (wPage << 13));

                                device.rtdefsubaddr(RTRegime.Transmit, wSubAddr);
                                for (ushort wAddr = 0; wAddr <= 31; wAddr++)
                                    withoutErrors &= device.rtgetw(wAddr) ==
                                                 (ushort)((wAddr + 32) | (wSubAddr << 8) | (wPage << 13));
                            }
                        }

                        Console.WriteLine("rtputw()/rtgetw() test {0}!", withoutErrors ? "OK" : "failed");
                    }
                    catch (MKODeviceException ex)
                    {
                        Console.WriteLine("{0}: {1}", ex.ErrorType, ex.Message);
                    }
                }
            }
        }
    }
}
