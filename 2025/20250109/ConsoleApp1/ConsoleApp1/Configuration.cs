using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public static class Configuration
    {
        public static string Setting1;
        public static int Setting2;

        // Static constructor
        static Configuration()
        {
            Console.WriteLine("Static constructor called.");
            Setting1 = "DefaultValue";
            Setting2 = 42;
        }
    }
}
