using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eclus.NET.MKO;

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
                    
                }
            }
        }
    }
}
