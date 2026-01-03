using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDotNet9
{
    public class Employee : ICloneable
    {
        public string Name { get; set; }
        //public Span<int> Test { get; }


        public object Clone()
        {
            return new Employee() { Name=Name };
        }

        public int PrintString(string str)
        {
            Console.WriteLine(str);
            return 1;
        }

    }
}
