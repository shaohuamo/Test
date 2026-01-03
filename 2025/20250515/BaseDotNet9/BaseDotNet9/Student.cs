using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDotNet9
{
    public struct Student
    {
        public  int Id;
        public  int Age =10;

        public Student()
        {
            Id = 1;
        }

        public Student(int id)
        {
            Id = id;
        }


    }

    public readonly struct Teacher
    {
        public readonly int Id;
        public readonly int Age;

        public Teacher()
        {
            
        }
    }

    file class Service
    {
        public string GetData() => "test data";
    }
}
