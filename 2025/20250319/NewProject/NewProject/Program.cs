using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using static NewProject.Program;
using Object = System.Object;

namespace NewProject
{
    internal class Program
    {
        //public delegate void FirstDelegate< in T>(T name);

        //public delegate void firstDelegate(string name);

        //public event firstDelegate fristEvent;

        //public event EventHandler<CarInfoEventArgs> NewCardInfo;

        static AsyncLocal<string> _contextData = new AsyncLocal<string>();

        static void Main(string[] args)
        {
            _contextData.Value = "Jeffrey";

            ThreadPool.QueueUserWorkItem(state =>
            {
                // Expected: "Jeffrey"
                Console.WriteLine("Name={0}", _contextData.Value); 
            });

            // Prevents context from flowing
            ExecutionContext.SuppressFlow(); 

            ThreadPool.QueueUserWorkItem(state =>
            {
                // Expected: NULL due to SuppressFlow
                Console.WriteLine("Name={0}", _contextData.Value); 
            });

            ExecutionContext.RestoreFlow(); // Restores context flow

            //try
            //{
            //    Task.WhenAll(
            //        Task.Run(() => throw new InvalidOperationException("First error")),
            //        Task.Run(() => throw new ArgumentException("Second error"))
            //    ).Wait();
            //}
            //catch (AggregateException ex)
            //{
            //    Console.WriteLine($"Main Exception: {ex.Message}");
            //    Console.WriteLine("Inner Exceptions:");

            //    foreach (var inner in ex.InnerExceptions)
            //    {
            //        Console.WriteLine($"  - {inner.GetType()}: {inner.Message}");
            //    }

            //    Console.WriteLine($"{ex.InnerException}");
            //}

            //var t = 'a' + 'b';

            //Int32 obj1 = Convert.ToInt32(args[0]);
            //Int32 obj2 = Convert.ToInt32(args[1]);
            //if (obj1 == obj2)
            //{
            //    Console.WriteLine("11");
            //}

            //var set = new HashSet<string?>();
            //set.Add(null);
            //var str = "a";
            //Int32 a = (int)('a');
            //var hashcode = str.GetHashCode();

            //int i = 1, j = 2,k = 3;
            //string l = "4";

            //string result1 = i + j + k + l;

            //string result2 = l+i + j + k;

            //string str = "a";
            //str += "b";
            //str += "c";

            //string a="a",b="b",c="c";
            //string str1 = a + b + c;
            //Console.WriteLine(str1);
            //string str2 = "a" + "b" + "c";
            //Console.WriteLine(str2);


            //MyStruct s = new MyStruct { Value = 10 };
            //s.Display(); // Direct method call

            //var str = "test";
            //str.ToString();


            //var dc = new Dictionary<string, int>(10);
            //dc.Add("number1",1);

            //Console.WriteLine((-1).GetHashCode());

            //var bitWise = ~1;

            //Int32? x = 5;
            //Int32 result = ((IComparable) x).CompareTo(5);

            //var my = new MyClass(1);

            //Point p;
            //p.y = 1;


            ////从null隐式转换为Nullable<Int32>
            //Int32? d = null;
            ////从非可空Int32隐式转换为Nullable<Int32>
            //Int32? c = 1;

            //FirstDelegate<Object> deO = name => Console.WriteLine(name);
            //FirstDelegate<string>  de = deO;
            //de("123");

            //IEnumerator<string> enumerator = null;

            //Object? obj = default(Object);


            //var action1 = new Action(test);

            //List<Action> actions = new List<Action>();
            //int outerCounter = 0;
            //for (int i = 0; i < 2; i++)
            //{
            //    int innerCounter = 0;
            //    Action action = () =>
            //    {
            //        Console.WriteLine(
            //            "Outer: {0}; Inner: {1}",
            //            outerCounter, innerCounter);
            //        outerCounter++;
            //        innerCounter++;
            //    };
            //    actions.Add(action);
            //}

            //actions[0]();
            //actions[0]();
            //actions[1]();
            //actions[1]();

            //Console.WriteLine(outerCounter);

            //publisher.myEvent(publisher,null);
            //var list = new List<string>();
            //list.Where(s => s.Length > 2);
            //int a = 2;
            //a.GetType();

            //var parent = new Parent();
            //if (parent is Child)
            //{
            //    Child p = (Child)parent;
            //    Console.WriteLine(p._childName);
            //}

            //var array = new int[10];
            //var currntEncoding = Encoding.Default;
            //char mytext = '种';//用字符表示字符'种'
            //Console.WriteLine(mytext);//output:'种'


            //int number = (int) mytext;//用十进制值表示字符'种'
            //Console.WriteLine(number);//output:31181

            //char unicode = '\u79CD';//用Unicode值表示字符'种'
            //Console.WriteLine(unicode);//output:'种'

            Console.ReadKey();
        }

        static void test()
        {
            Console.WriteLine(1);
        }

        static void Read<T>()
        {
            var value = default(T);
        }

        static void SomeMethod()
        {
            throw new InvalidOperationException("Something went wrong!");
        }

        static void LogException(Exception ex,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            Console.WriteLine($"Occurred in method: {memberName}");
            Console.WriteLine($"Source file: {filePath}");
            Console.WriteLine($"Line number: {lineNumber}");
        }
    }

    struct Point
    {
        public int x = 2, y = 0;

        public Point()
        {
            
        }
        //public Point(int m)
        //{
        //    y = x = m;
        //    this = new Point();
        //}
    }

    class MyClass
    {
        public string Name { get; set; }


        ref struct MyStruct
        {
            public int Age;
        }
        //public int a = 1, b = 2;

        //public MyClass()
        //{

        //}

        //public MyClass(int x)
        //{

        //}
    }

    class Parent
    {
        private int x;
        public string _parentName;

        public Parent()
        {
            x = 1;
            _parentName = "parent";
        }

        ~Parent()
        {

        }
    }

    class Child : Parent
    {
        private int y;
        public string _childName;

        public Child()
        {
            y = 2;
            _childName = "child";
        }
    }

    internal static class BoxingForInterfaceMethod
    {
        private struct Point : IComparable
        {
            private Int32 m_x, m_y;

            // Constructor to easily initialize the fields
            public Point(Int32 x, Int32 y)
            {
                m_x = x;
                m_y = y;
            }

            // Override ToString method inherited from System.ValueType
            public override String ToString()
            {
                // Return the point as a string
                return String.Format("({0}, {1})", m_x, m_y);
            }

            // Implementation of type-safe CompareTo method
            public Int32 CompareTo(Point other)
            {
                // Use the Pythagorean Theorem to calculate 
                // which point is farther from the origin (0, 0)
                return Math.Sign(Math.Sqrt(m_x * m_x + m_y * m_y)
                                 - Math.Sqrt(other.m_x * other.m_x + other.m_y * other.m_y));
            }

            // Implementation of IComparable’s CompareTo method
            public Int32 CompareTo(Object o)
            {
                if (GetType() != o.GetType())
                {
                    throw new ArgumentException("o is not a Point");
                }

                // Call type-safe CompareTo method
                return CompareTo((Point)o);
            }
        }
    }

    public class CarInfoEventArgs : EventArgs
    {
        public CarInfoEventArgs(string car)
        {
            Car = car;
        }

        public string Car { get; }
    }



    class TestA : ITest
    {
        public virtual int Add(int x, int y)
        {
            throw new NotImplementedException();
        }
    }

    interface ITest
    {
        int Add(int x, int y);
    }

    abstract class TestB : ITest
    {
        public abstract int Add(int x, int y);
    }

    //class GenericClass< out T>
    //{

    //}

    struct MyStruct
    {
        public int Value;

        public void Display()
        {
            Console.WriteLine($"Value: {Value}");
        }
    }

    class CustomersList<T> : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


}
