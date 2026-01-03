using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Student(string name,DateTime dateOfBirth, int age)
    {
        public string GetDetails()
        {
            return $"name : {name} dateOfBirth : {dateOfBirth} age : {age}";
        }
    }

    record Teacher(string name, DateTime dateOfBirth, int age)
    {
        public int Weight { get; set; }
    }
}
