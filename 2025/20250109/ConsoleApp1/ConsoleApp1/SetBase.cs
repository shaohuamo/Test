using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class SetBase
    {
        private readonly Int32 m_length = 0;
        public Int32 Find(Object value)
        {
            return Find(value, 0, m_length);
        }

        public virtual Int32 Find(object value, int i, int mLength)
        {
            Console.WriteLine($"this is from SetBase value is {value}");
            return mLength;
        }
    }
}
