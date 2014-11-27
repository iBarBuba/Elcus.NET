using System;
using System.Linq;
using Eclus.NET.MKO;
using Eclus.NET.MKO.Data.Events;
using Eclus.NET.MKO.Enums;
using Eclus.NET.MKO.Exceptions;

namespace SimpleTest
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                MKO mko;
                using (mko = new MKO())
                {
                    using (var device = mko[mko.GetPossibleDeviceNumbers().FirstOrDefault()])
                    {
                        //режим ОУ
                        device.rtreset();
                        Console.WriteLine("rtreset() successfully!");
                        var wMaxPage = device.rtgetmaxpage();
                        Console.WriteLine("rtMaxPage = {0}", wMaxPage);
                        for (ushort wPage = 0; wPage <= wMaxPage; wPage++)
                        {
                            device.rtdefpage(wPage);
                            for (ushort wSubAddr = 0; wSubAddr <= 0x1F; wSubAddr++)
                            {
                                device.rtdefsubaddr(RTRegime.Receive, wSubAddr);
                                for (ushort wAddr = 0; wAddr <= 31; wAddr++)
                                    device.rtputw(wAddr, (ushort) (wAddr | (wSubAddr << 8) | (wPage << 13)));
                                device.rtdefsubaddr(RTRegime.Transmit, wSubAddr);
                                for (ushort wAddr = 0; wAddr <= 31; wAddr++)
                                    device.rtputw(wAddr, (ushort) ((wAddr + 32) | (wSubAddr << 8) | (wPage << 13)));
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
                                                     (ushort) ((wAddr) | (wSubAddr << 8) | (wPage << 13));

                                device.rtdefsubaddr(RTRegime.Transmit, wSubAddr);
                                for (ushort wAddr = 0; wAddr <= 31; wAddr++)
                                    withoutErrors &= device.rtgetw(wAddr) ==
                                                     (ushort) ((wAddr + 32) | (wSubAddr << 8) | (wPage << 13));
                            }
                        }

                        Console.WriteLine("rtputw()/rtgetw() test {0}!", withoutErrors ? "OK" : "failed");

                        for (ushort wPage = 0; wPage <= wMaxPage; wPage++)
                        {
                            device.rtdefpage(wPage);
                            for (ushort wSubAddr = 0; wSubAddr <= 31; wSubAddr++)
                            {
                                device.rtdefsubaddr(RTRegime.Receive, wSubAddr);
                                var buf = new ushort[32];
                                for (ushort wAddr = 0; wAddr <= 31; wAddr++)
                                    buf[31 - wAddr] = (ushort) (wAddr | (wSubAddr << 8) | (wPage << 13));
                                device.rtputblk(0, buf);

                                device.rtdefsubaddr(RTRegime.Transmit, wSubAddr);
                                for (ushort wAddr = 0; wAddr <= 31; wAddr++)
                                    buf[31 - wAddr] = (ushort) ((wAddr + 32) | (wSubAddr << 8) | (wPage << 13));
                                device.rtputblk(0, buf);
                            }
                        }

                        withoutErrors = true;
                        for (ushort wPage = 0; wPage <= wMaxPage; wPage++)
                        {
                            device.rtdefpage(wPage);
                            for (ushort wSubAddr = 0; wSubAddr <= 31; wSubAddr++)
                            {
                                device.rtdefsubaddr(RTRegime.Receive, wSubAddr);
                                var buf = new ushort[32];
                                device.rtgetblk(0, ref buf);
                                for (ushort wAddr = 0; wAddr <= 31; wAddr++)
                                    withoutErrors &= buf[31 - wAddr] ==
                                                     (ushort) ((wAddr) | (wSubAddr << 8) | (wPage << 13));

                                device.rtdefsubaddr(RTRegime.Transmit, wSubAddr);
                                buf = new ushort[32];
                                device.rtgetblk(0, ref buf);
                                for (ushort wAddr = 0; wAddr <= 31; wAddr++)
                                    withoutErrors &= buf[31 - wAddr] ==
                                                     (ushort) ((wAddr + 32) | (wSubAddr << 8) | (wPage << 13));
                            }
                        }

                        Console.WriteLine("rtputblk()/rtgetblk() test {0}!", withoutErrors ? "OK" : "failed");

                        Console.WriteLine();
                        device.bcreset();
                        Console.WriteLine("bcreset() successfully!");
                        Console.WriteLine("bcMaxBase = {0}", device.bcgetmaxbase());

                        for (ushort wBase = 0; wBase < 5; wBase++)
                        {
                            device.bcdefbase(wBase);
                            for (ushort wAddr = 0; wAddr <= 63; wAddr++)
                            {
                                device.bcputw(wAddr, (ushort) (wAddr | (wBase << 6)));
                            }
                        }

                        withoutErrors = true;
                        for (ushort wBase = 0; wBase < 5; wBase++)
                        {
                            device.bcdefbase(wBase);

                            for (ushort wAddr = 0; wAddr <= 63; wAddr++)
                            {
                                withoutErrors &= ((ushort) (wAddr | (wBase << 6)) == device.bcgetw(wAddr));
                            }
                        }

                        Console.WriteLine("bcputw()/bcgetw() test {0}!", withoutErrors ? "OK" : "Failed");

                        withoutErrors = true;
                        ushort[] bcBuf;
                        for (ushort wBase = 0; wBase <= 5; wBase++)
                        {
                            bcBuf = new ushort[64];
                            device.bcdefbase(wBase);
                            for (ushort wAddr = 0; wAddr < 64; wAddr++)
                            {
                                bcBuf[63 - wAddr] = (ushort) (wBase + (wAddr << 6));
                            }
                            device.bcputblk(0, bcBuf);
                        }

                        for (ushort wBase = 0; wBase <= 5; wBase++)
                        {
                            bcBuf = new ushort[64];
                            device.bcdefbase(wBase);
                            device.bcgetblk(0, ref bcBuf);
                            for (ushort wAddr = 0; wAddr < 64; wAddr++)
                                withoutErrors &= ((ushort) (wBase + (wAddr << 6)) == bcBuf[63 - wAddr]);
                        }

                        Console.WriteLine("bcputblk()/bcgetblk() test {0}!", withoutErrors ? "OK" : "Failed");

                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Testing interrupts");
                        device.bcreset();
                        var evd = new TmkEventData();

                        device.tmkgetevd(ref evd);
                        Console.WriteLine("Int: {0}", evd.Int);

                        device.bcdefbase(0);
                        device.bcputw(0, 0xFFE1);
                        device.bcstart(0, CtrlCode.CTRL_C_BRCST);

                        switch (device.WaitForEvents(1000))
                        {
                            case MKOEvents.Object:
                                Console.WriteLine("We get interrupt...");
                                device.tmkgetevd(ref evd);
                                Console.WriteLine("Int: {0}", evd.Int);
                                break;
                            case MKOEvents.Timeout:
                                Console.WriteLine("Timeout");
                                break;
                            default:
                                Console.WriteLine("No interrrupts");
                                break;
                        }
                    }
                }
            }
            catch (MKODeviceException ex)
            {
                Console.WriteLine("{0}: {1}", ex.ErrorType, ex.Message);
            }
        }
    }
}
