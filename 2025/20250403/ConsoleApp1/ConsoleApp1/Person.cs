using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Person
    {
        public required string Name { get; set; }
        public int Age { get; set; }
        public Test Test {
            get
            {
                return new Test();
            }
            set
            {

            } }

        [SetsRequiredMembers]
        public Person()
        {
            Name = "Unknown";
        }

        [SetsRequiredMembers]
        public Person(string personName)
        {
            Name = personName;
        }
    }

    public ref struct Test
    {
        private ref int x;
    }
}
