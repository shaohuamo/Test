using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public sealed class SomeType
    {                                 //  1
        // Nested class
        private class SomeNestedType { }                            //  2

        // Constant, read-only, and static read/write field
        private const Int32 c_SomeConstant = 1;                     //  3
        private readonly String m_SomeReadOnlyField = "2";          //  4
        private static Int32 s_SomeReadWriteField = 3;              //  5

        // Type constructor
        static SomeType() { }                                       //  6

        // Instance constructors
        public SomeType() { }                                       //  7
        public SomeType(Int32 x) { }                                //  8


        // Static and instance methods
        public static void Main1() { }                               //  9

        public String InstanceMethod() { return null; }             // 10

        // Instance property
        public Int32 SomeProp
        {                                     // 11
            get { return 0; }                                        // 12
            set { }                                                  // 13
        }

        // Instance parameterful property (indexer)
        public Int32 this[String s]
        {                               // 14
            get { return 0; }                                        // 15
            set { }                                                  // 16
        }

        // Instance event
        public event EventHandler SomeEvent;                        // 17
    }
}
