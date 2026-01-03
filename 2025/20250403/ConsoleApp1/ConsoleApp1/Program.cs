using System.Runtime.CompilerServices;

namespace ConsoleApp1
{
    //public record Person(string FirstName, string LastName, int Age);

    public delegate void FeedBack(Int32 value);
    internal class Program
    {
        public delegate void  FeedBack(Int32 value);

        static readonly object _lock = new object();

        static  int _count = 0;
        static void Main(string[] args)
        {
            //---------------------------------------------------------
            //collection expression
            //int[] array = [1, 2, 3, 4, 5];//创建int[]实例
            //List<int> list = [1, 2, 3, 4, 5];//创建list实例
            //Span<int> span1 = [1, 2, 3, 4, 5];//创建Inline数组(本质上是一个struct)的实例
            //IList<int> list1 = [1, 2, 3, 4, 5];//创建list实例
            //ICollection<int> collection = [1, 2, 3, 4, 5];//创建list实例

            //---------------------------------------------------------
            //spread operator
            //Span<int> span2 = [1, 2, 3, 4, 5];
            //Span<int> span3 = [1, 2, 3, 4, 5];
            //Span<int> span4 = [.. span1,..span2,..span3];//会创建新的数组实例
            //span4[1] = 100;
            //Console.WriteLine(span1[1]);

            //List<int> list1 = [1, 2, 3, 4, 5];
            //List<int> list2 = [1, 2, 3, 4, 5];
            //List<int> list3 = [1, 2, 3, 4, 5];
            //List<int> list4 = [.. list1, .. list2, .. list3];//会创建新的list实例
            //list4[1] = 100;
            //Console.WriteLine(list1[1]);


            //---------------------------------------------------------
            //range operator
            //int[] array1 = [1, 2, 3, 4, 5];
            //var slice = array1[1..4];//会创建新的数组实例
            //slice[0] = 100;
            //Console.WriteLine(array1[1]);//2

            //Span<int> span = [1, 2, 3, 4, 5];
            //var slice2 = span[1..4];//不会创建新的数组实例
            //slice2[0] = 100;
            //Console.WriteLine(span[1]);//100


            //-------------------------------------------------------
            //var f = List<int> (string name, int age = 20) => [1, 2, 3, 4];

            //-----------------------------------------------
            //在lambda表达式中获取迭代变量
            //var actions = new List<Action>();

            //for (int i = 0; i < 5; i++)
            //{
            //    //int temp = i;
            //    actions.Add(()=>Console.WriteLine(i));
            //}

            //for (int i = 0; i < 5; i++)
            //{
            //    int temp = i;
            //    actions.Add(() => Console.WriteLine(temp));
            //}


            //foreach (var action in actions)
            //{
            //    action();
            //}

            //-----------------------------------------------------
            //int[] Array = [1, 2, 3, 4, 5, 6, 7, 8, 9, 0];

            //var result = Array.Length;
            //Int32 a = Convert.ToInt32("abc");
            //Console.WriteLine(a);
            //FeedBack de = new FeedBack(FeedBackStatic);
            //de += FeedBackStatic2;
            //de.Invoke(1);
            //List<string> b = ["one", "two", "three"];
            //IList<string> list = ["a", "b", "c"];

            //ICollection<int> collection = [1, 2, 3];

            //------------------------------------------------------------------------
            //ref字段
            //int value = 42;
            //RefStruct refStruct = new RefStruct(ref value);
            ////refStruct.MyField = ref _count;

            //Console.WriteLine(refStruct.MyField); //42
            //Console.WriteLine(value); //42

            //value = 100;

            //Console.WriteLine(refStruct.MyField); //100
            //Console.WriteLine(value); //100

            //refStruct.MyField = 200;
            //Console.WriteLine(refStruct.MyField); //200
            //Console.WriteLine(value); //200

            //-------------------------------------------------------------------------
            //Point p = new Point();

            //Person person = new Person();

            //Service service = new Service();
            //Console.WriteLine(service.GetData());

            //AdditionalService

            //{
            //    Console.WriteLine(new List<int>() { 10, 20, 50 } is [10, var b, var c] && c > b); //true
            //}
            //{
            //    Console.WriteLine(new List<int>() { 10, 20, 50 } is [10, var b and >= 20, var c] && c > b); //true
            //}

            //Person person = new Person("John", "Smith", 20);
            //Console.WriteLine(person is ("John", "Smith", >= 18) 
            //    and var (firstName, lastName, age) && firstName != lastName); //true

            //Object value = 1;
            //if (value is var v && v is int i && i > 0)
            //{
            //    Console.WriteLine($"Positive int: {i}");
            //}

            //if (value is int a and > 0)
            //{
            //    Console.WriteLine($"Positive int: {a}");
            //}

            //var allLists = new List<int>();
            //Int32 min, max;
            //min = max = 10;
            //allLists.Where(list => list is var count && count >= min && count <= max);
            //Console.ReadKey();


            //string json1 = """
            //               {
            //                 "name": "John Doe",
            //                 "age": 35
            //               }
            //               """;
            //Console.WriteLine(json1);

            //string name = "Alice";
            //int age = 25;
            //string message = $"""""  
            //                  Hello, {name}, 
            //                   age """" is {age}
            //                  """"";
            //var sharedList = new List<int>();

            //lock (_lock)
            //{
            //    var tempList = new List<int>(sharedList);
            //    tempList.Add(1);
            //    tempList.Add(2); // could throw
            //    sharedList = tempList; // atomic swap
            //}

            //Console.WriteLine(sharedList.Count);
            //// Fast thread hogging the lock
            //new Thread(() => Worker("🚀 FastThread", 50)) { IsBackground = true }.Start();

            //// Starving thread
            //new Thread(() => Worker("🐢 StarvingThread", 500)) { IsBackground = true }.Start();

            //Thread.Sleep(5000);
            //Console.WriteLine("Press Enter to exit");
            Console.ReadLine();



            //var fb1 = new FeedBack(FeedBackStatic);
            //var fb2 = new FeedBack(new Person().FeedBackInstance);
            //fb1(10);
            //fb2(10);
        }

        static void Worker(string name, int delay)
        {
            while (true)
            {
                lock (_lock)
                {
                    Console.WriteLine($"{name} acquired the lock");
                    Thread.Sleep(delay);
                }
                Thread.Sleep(10); // Yield to other threads
            }
        }

        static void FeedBackStatic(Int32 value)
        {
            Console.WriteLine("this is a static method call");
        }

        static void FeedBackStatic2(Int32 value)
        {
            Console.WriteLine("this is a static method call");
        }




    }
    class class2 { }

    class class1(class2 classt)
    {
        private readonly class2 _class2 = classt;
    }

    //class Person
    //{
    //    public void FeedBackInstance(Int32 value)
    //    {
    //        Console.WriteLine("this is a instance method call");
    //    }
    //}
}
