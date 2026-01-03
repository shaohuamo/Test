using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public ref struct RefStruct
    {
        public ref int MyField;

        public RefStruct(ref int refField)
        {
            MyField = ref refField;
        }
    }
}
