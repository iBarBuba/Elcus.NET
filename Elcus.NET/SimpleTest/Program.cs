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
            var device = MKO.Find();
            device.Open();
            device.Close();
        }
    }
}
