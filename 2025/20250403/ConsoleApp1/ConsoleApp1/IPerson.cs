using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IPerson
    {
        string GetName();//没有具体实现

        public int GetAge() => GetHeight();//有具体实现

        public static int GetWeight() => 100;

        private int GetHeight() => 50;
    }
}
