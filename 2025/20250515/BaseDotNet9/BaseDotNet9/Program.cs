using System.Data;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Data.SqlClient;
using ProductInfo = (string name,decimal price,int stock);

namespace BaseDotNet9
{
    public delegate string PrintString();
    public delegate void MyDelegateType(int a, int b);

    

    public class Program
    {
        public event EventHandler<string>? EventTest;
        public event MyDelegateType? myEvent;

        public Span<int> Array => new Span<int>(null);//使用了newobj创建值类型的实例

        static void Main(string[] args)
        {
            //--------------------------------------------
            //file class
            Service s = new();
            Console.WriteLine(s.GetData());
            

            //-------------------------------------------------------------------
            //Raw string literal
            //不需要手动写转义字符
            //var singleLine = """
            //                 This is a "raw string literal". 
            //                 It can contain characters like \, ' and ".
            //                 """;

            //Console.WriteLine(singleLine);
            Console.ReadKey();
            //------------------------------------------------------------------------
            //由于Combine方法中使用了一个随机seed,所以每次生成的hashcode值都不同
            //var hashcode = HashCode.Combine(100, "lambda");
            //Console.WriteLine(hashcode);

            //-------------------------------------------------------
            //信号量
            //var semaphore = new Semaphore(0,3);

            //NullTest(null);
            //span
            //int[] array = null;
            //SpanTest(array);


            //-------------------------------------------------------------
            //task cancellation
            //var cts1 = new CancellationTokenSource();
            ////Run方法中的第二个参数cts1.Token用于在Task还未开始时取消该Task
            ////而Sum方法中的第一个参数是用于在Task开始后取消该Task
            //Task<int> t = Task.Run(() => Sum(cts1.Token, 100000), cts1.Token);
            //cts1.Cancel();
            //try
            //{
            //    Console.WriteLine("the sum is " + t.Result);
            //}
            //catch (AggregateException x)
            //{
            //    x.Handle(e => e is OperationCanceledException);
            //    Console.WriteLine("Sum was cancelled");
            //}

            //thread cancellation
            //var cts2 = new CancellationTokenSource();
            //ThreadPool.QueueUserWorkItem(_ => Count(cts2.Token, 1000));

            //Thread.Sleep(200);
            //cts2.Cancel();

            //----------------------------------------------------------------------------
            //反射
            //var t = typeof(Employee);
            //dynamic? employee = Activator.CreateInstance(t);
            //int result = employee?.PrintString("this instance is created by reflection");

            //StringBuilder sb = new();


            //可空值类型  也是值类型
            //int? d = null;//CLR为所有的值类型都提供了一个无参的构造函数
            //int? c = 1;
            //----------------------------------------------------------------
            //ref传递值类型变量时，可以在方法中为该变量赋值一个新的值类型实例
            //var s = new Student
            //{
            //    Age =20, Id = 1002
            //};
            //RefStructure(ref s);
            //Console.WriteLine(s.Id);


            //------------------------------------------------------------------
            //值类型的size
            //unsafe
            //{
            //    var size = sizeof(Student);
            //}

            //-------------------------------------------------------------------
            //try-finally statement
            //using (var connection = new SqlConnection(""))
            //{

            //}

            //--------------------------------------------------------------------
            //CallerInfo特性
            //Log("");


            //单个字符与其对应的Unicode码值之间的相互转换
            //char unicodeCharacter = '中';
            //int unicodeValue = (int)unicodeCharacter;
            //char unicodeC2 = (char)unicodeValue;

            //-----------------------------------------------------------------------
            //reference变量
            //int a = 10;
            //ref int alias = ref a;
            //ReadOnlySpan<char> s = "123";


            //int[] array = new int[] { 1,2,3,4,5};
            //var span = array.AsSpan();


            //-----------------------------------------------------------------------
            //GroupBy
            //List<Person> people = new List<Person>
            //{
            //    new Person { Name = "Alice", Age = 25, City = "New York" },
            //    new Person { Name = "Bob", Age = 30, City = "Chicago" },
            //    new Person { Name = "Charlie", Age = 25, City = "New York" },
            //    new Person { Name = "David", Age = 30, City = "Chicago" },
            //    new Person { Name = "Eve", Age = 25, City = "Los Angeles" }
            //};

            //// 按城市分组
            //var groupsByCity = people.GroupBy(p => p.City).Where(g => g.Count() > 1)
            //    .Select(g => g.Key);

            ////实现LINQ的2种不同语法

            ////LINQ Query Operators
            //var persons1 = people.
            //    Where(p => p.Age > 25).
            //    OrderBy(p => p.City).
            //    Select(p=>p.Name);

            ////LINQ Extension Methods
            //var persons2 =
            //    from p in people
            //    where p.Age > 25
            //    orderby p.City
            //    select p.Name;

            //foreach (var p in persons1)
            //{
            //    Console.WriteLine(p);
            //}

            //foreach (var p in persons2)
            //{
            //    Console.WriteLine(p);
            //}


            //-------------------------------------------------------
            //扩展方法
            //var name = "lambda_zb";
            //name.Foo();

            //-------------------------------------------------------
            //枚举器和迭代器
            //var persons = new List<string> { "123", "456" };

            ////枚举器
            ////var it = persons.GetEnumerator();
            //foreach (var person in persons)
            //{
            //    Console.WriteLine(person);
            //}

            ////迭代器
            //foreach (var item in new CustomCollection())
            //{
            //    Console.WriteLine(item);
            //}

            //foreach (var item in new CustomCollection().Reserve())
            //{
            //    Console.WriteLine(item);
            //}

            //--------------------------------------------------------
            //int o = 2;
            //o.ToString();
            //int a = 2;
            //var student = new Student();
            //var s2 = student;//值类型的赋值
            //var e = new Employee();

            //student.ToString();//调用继承自ValueType的ToString方法，不需要装箱
            //student.GetType();//调用继承自Object的GetType方法，需要装箱


            //var student = new Student { Age = 10, Id = 100 };



            //----------------------------------------------
            //foreach和for循环中的Lambda表达式
            //var names = new List<string> { "x", "y", "z" };
            //var actions = new List<Action>();
            //foreach (var name in names)
            //{
            //    actions.Add(() => Console.WriteLine(name));
            //}

            //for (int i = 0; i < names.Count; i++)
            //{
            //    int a = i;
            //    actions.Add(() => Console.WriteLine(names[a]));
            //}
            //foreach (var action in actions)
            //{
            //    action();
            //}


            //-----------------------------------------------------
            //委托推断
            //int x = 10;
            //PrintString firstMethod = x.ToString;
            //firstMethod();

            ////泛型委托的协变和逆变
            ////协变——返回类型  逆变——参数类型
            //Func<Derived, Base> myDelegate = new Func<Base, Derived>(DelegateFunction);
            //Base derivedResult = myDelegate(new Derived());

            ////将Lambda表达式赋值给一个委托类型的变量
            //Predicate<int> lambda = param => param > 10;

            //闭包
            //int i = 5;
            //Func<int, int> f = x => x + i;

            //---------------------------------------------------
            //CLR生成的int[]类型隐式地派生自IList<int>
            //var list = new int[] { 1, 2, 3, 4 };
            //UpCasting(list);



            //----------------------------------------------------
            //继承结构中的type constructor
            //var d = new Derived();

            //-----------------------------------------------------
            //按位运算符
            //int a = 5, b = 10;
            //a = a ^ b;
            //b=a ^ b;
            //a=a ^ b;
            //Console.WriteLine(a);
            //Console.WriteLine(b);

            //------------------------------------------------------
            //??运算符
            //int? a = null;
            //var b = a ?? 1;

            //Console.WriteLine(b);



            //-------------------------------------------------
            //可选参数
            //Test();


            //------------------------------------------------
            //var builder = new StringBuilder();
            //builder.Append("start");
            //builder.AppendLine();//换行
            //builder.Append("end");
            //Console.WriteLine(builder.ToString());

            //------------------------------------------------
            //判断字符串是否为空
            //string s3 = " ";
            //string s4 = "  \t\n  ";

            //Console.WriteLine(string.IsNullOrEmpty(s3)); // False (纯空白字符不视为空)
            //Console.WriteLine(string.IsNullOrWhiteSpace(s3)); // True (纯空白字符)
            //Console.WriteLine(string.IsNullOrWhiteSpace(s4)); // True (混合空白字符)


            //-------------------------------------------------
            //string intern
            //string x = "123";
            //string y = "123";
            //if (object.ReferenceEquals(x,y))//true
            //{
            //    Console.WriteLine();
            //}

            //string a = "123".Substring(0, 2);
            //string b = "123".Substring(0, 2);

            //if ((object)a == (object)b)
            //{
            //    Console.WriteLine("a和b的引用相同");
            //}
            //else
            //{
            //    Console.WriteLine("a和b的引用不同");
            //}

            //if (a == b)
            //{
            //    Console.WriteLine("a和b的值相同");
            //}
            //else
            //{
            //    Console.WriteLine("a和b的值不同");
            //}

            //int c=int.Parse(a);
            //int d=int.Parse(b);
            //if (c==d)
            //{
            //    Console.WriteLine("c和d的值相同");
            //}
            //string z = "12345".Substring(0, 3);
            //if (object.ReferenceEquals(x, z))//false
            //{
            //    Console.WriteLine();
            //}



            //----------------------------------------------------
            //var array = new Employee[5];

            //Type type = typeof(Employee[]);
            //MethodInfo[]? addMethod = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            //var deepCopy = new Employee[array.Length];
            //for (int i = 0; i < array.Length; i++)
            //{
            //    deepCopy[i] = (Employee)array[i].Clone();
            //}

            //Add(array);


            //--------------------------------------------------
            //object o = "abc";

            //switch (o)
            //{
            //    case 1:
            //        Console.WriteLine("o is integer");
            //        break;
            //    case "abc":
            //        Console.WriteLine("o is string");
            //        break;
            //    default:
            //        Console.WriteLine("o is null");
            //        break;
            //}


            //--------------------------------------------------
            //var dateTime = new DateTime(2025, 8, 1);

            //var now = DateTime.Now;
            //var today = DateTime.Today;

            //bool result = int.TryParse("123", out int number);

            //----------------------------------------------------
            //int a = 10;
            //object b = a;
            //IConvertible c = a;

            //已装箱的值类型其类型依旧是对应的值类型
            //Console.WriteLine(a.GetType());//System.Int32
            //Console.WriteLine(b.GetType());//System.Int32
            //Console.WriteLine(c.GetType());//System.Int32

            //--------------------------------------------------
            //ADO.NET
            //var connectionString =
            //    "Data Source=SKY-20190427UDK;Initial Catalog=Practice;User ID=sa;Password=123456;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            ////ExecuteReader
            //using (var connection = new SqlConnection(connectionString))
            //{
            //    var query = "SELECT * FROM dbo.SC WHERE Cno=@Cno";
            //    using (var command = new SqlCommand(query, connection))
            //    {
            //        command.Parameters.AddWithValue("@Cno", "1001");

            //        connection.Open();
            //        using (var reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                Console.WriteLine($"Sno: {reader["Sno"]}, Cno: {reader["Cno"]}, Grade: {reader["Grade"]}");
            //            }
            //        }
            //    }
            //}


            //var dataTable = new DataTable();

            //using (var connection = new SqlConnection(connectionString))
            //{
            //    var query = "SELECT * FROM dbo.SC";
            //    var adapter = new SqlDataAdapter(query, connection);

            //    adapter.Fill(dataTable);
            //}

            //foreach (DataRow row in dataTable.Rows)
            //{
            //    Console.WriteLine($"Sno: {row["Sno"]}, Cno: {row["Cno"]}, Grade: {row["Grade"]}");
            //}

            //Console.WriteLine();
            //------------------------------------------------
            //is运算符
            //object o = "12";

            //if (o is string number)
            //{
            //    Console.WriteLine(number);
            //}

            //if (o is string)
            //{
            //    string number2 = (string)o;
            //    Console.WriteLine(number2);
            //}
            //string? number1 = o as string;
            //if (number1 != null)
            //{
            //    Console.WriteLine(number1);
            //}





            //------------------------------------------------
            //int a = 1;
            //char b = 'a';
            //string c = "this";
            //bool d = false;

            //var result1 = a + b;
            //var result2 = a + c;
            //var result3 = b+ c;
            //var result4 = d+ c;
            //string text = """
            //              "name":"lambda"
            //              """;


            //------------------------------------------------
            //var dic = new Dictionary<string, int>();
            //dic.Add("test1",1);
            //dic.Add("test2",1);


            //------------------------------------------------
            //var result = BitOperations.Log2(1005);
            //int[] array = new int[50];
            //for (int i = 0; i < array.Length; i++)
            //{
            //    array[i] = 50-i;
            //}
            //Array.Sort(array);
        }

        private static int Sum(CancellationToken token, int n)
        {
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                token.ThrowIfCancellationRequested();

                checked
                {
                    sum += i;
                }
            }
            return sum;
        }

        private static void Count(CancellationToken token, int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Count was cancelled");
                    break;
                }
                Console.WriteLine(i);
            }
        }

        public Task<string> GetHttp()
        {
            return Task.Run(async () =>
            {
                HttpResponseMessage msg = await new HttpClient().GetAsync("http://www.baidu.com");
                Console.WriteLine("test message");
                return await msg.Content.ReadAsStringAsync();
            });
        }

        //public static void Test(int x = 1)
        //{
        //    Console.WriteLine();
        //}

        //public static void NullCoalescingOperator(Int32? b = null, params int[] array)
        //{
        //    Int32 x = b ?? 123;//x=b.GetValueOrDefault(123)
        //    Console.WriteLine(x);
        //}

        //public static void UpCasting(IList<int> list)
        //{
        //    list.Add(1);
        //}

        //public void RaiseEvent()
        //{
        //    //以线程安全的方式引发事件
        //    var temp = Volatile.Read(ref EventTest);
        //    temp?.Invoke(this, "test");
        //}

        //public static void Log(
        //    string message,
        //    [CallerMemberName] string member = "",
        //    [CallerFilePath] string file = "",
        //    [CallerLineNumber] int line = 0)
        //{
        //    Console.WriteLine($"{file}({line}): {member} -> {message}");
        //}

        //public static void RefStructure(ref Student s)
        //{
        //    s = new Student() { Age = 20, Id = 1001 };
        //}

        // Web场景：通常推荐 ExecuteReader
        //public async Task<List<int>> GetProductsForWebAsync()
        //{
        //    var products = new List<int>();

        //    using (var conn = new SqlConnection("connectionString"))
        //    {
        //        await conn.OpenAsync();
        //        using (var cmd = new SqlCommand("SELECT ProductID, ProductName, UnitPrice FROM Products", conn))
        //        using (var reader = await cmd.ExecuteReaderAsync())
        //        {
        //            while (await reader.ReadAsync())
        //            {
        //                //
        //            }
        //        }
        //    }

        //    return products; // 返回DTO，不是DataTable
        //}

        public static void Add(ICollection<Employee> employees)
        {
            var newEmployee = new Employee();
            employees.Add(newEmployee);
        }

        public static Derived DelegateFunction(Base b)
        {
            return new Derived();
        }

        public static void SpanTest(Span<int> array)
        {

        }

        public static void NullTest(Action action)
        {

        }

        public static void RefReadOnly(in int i)
        {
            Console.WriteLine(i);
        }


    }

    public class Base
    {
        static Base()
        {
            Console.WriteLine("Base type constructor");
        }
    }

    public class Derived : Base
    {
        static Derived()
        {
            Console.WriteLine("Derived type constructor");
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        public Person ShallowCopy()
        {
            return (Person)MemberwiseClone();
        }
    }

    file class Service
    {
        public string GetData() => "test data";
    }
}
