using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp1.Program;

namespace ConsoleApp1
{
    public class Student
    {
        

        public void Add(Int32 value)
        {
            ITest test=null;
            test.ToString();
        }
        //private static Int32 s_x = 5;
        //public void test1(int value)
        //{
        //    test2();
        //}

        //public void test2()
        //{
        //    Console.WriteLine(s_x);
        //}
        //public IEnumerator<string> GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}
    }

    interface ITest
    {
        void Test();
    }

    abstract class ClassP
    {

    }

    class ClassC : ClassP
    {
        //private object obj = new ClassP();
    }
    class ClassA
    {
        private void Test() { }
    }

    class classB : ClassA
    {
        public void test1()
        {
            //base.Test();
        }
    }

    /*[Flags] */    // The C# compiler allows either "Flags" or "FlagsAttribute".
    public enum Actions
    {
        Read = 0x0001,
        Write = 0x0002,
        Delete = 0x0004,
        ReadWrite = Actions.Read | Actions.Write,
        Query = 0x0008,
        Sync = 0x0010
    }

    class StaticClass
    {
        public delegate void Feedback(Int32 value);

        private static void FeedbackToConsole(Int32 value)
        {
            Console.WriteLine("Item=" + value);
        }
        internal static Feedback GetFeedBackDelegate()
        {
            return FeedbackToConsole;
        }
    }


}
