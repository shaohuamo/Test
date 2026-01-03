using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDotNet9
{
    public static class StringExtensions
    {
        public static void Foo(this string s)
        {
            Console.WriteLine($"Foo invoked for {s}");
        }
    }
}
