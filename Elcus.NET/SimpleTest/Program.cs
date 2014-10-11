using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eclus.NET.MKO;
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
                        Console.WriteLine(device.tmkgetmode());
                        device.bcreset();
                        Console.WriteLine(device.tmkgetmode());
                        device.rtreset();
                        Console.WriteLine(device.tmkgetmode());
                        device.mtreset();
                        Console.WriteLine(device.tmkgetmode());
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
