using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class SetChild: SetBase
    {
        public override int Find(object value, int i, int mLength)
        {
            Console.WriteLine($"this is from SetChild value is {value}");
            return mLength;
        }
    }
}
