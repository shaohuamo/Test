using System.Security.Cryptography.X509Certificates;
using ClassLibrary1;

namespace ConsoleApp1
{
    internal class Program
    {
        public delegate void Feedback(Int32 value);

        static  void Main(string[] args)
        {
            Byte[] bytesToWrite = new Byte[] { 1, 2, 3, 4, 5 };

            // Create the temporary file.
            FileStream fs = new FileStream("Temp.dat", FileMode.Create);

            // Write the bytes to the temporary file.
            fs.Write(bytesToWrite, 0, bytesToWrite.Length);

            // Explicitly close the file when done writing to it.
            ((IDisposable)fs).Dispose();

            // Delete the temporary file.
            File.Delete("Temp.dat");

            //var myClass = new ParentClass();
            //myClass.ToString();

            //var child = new ChildClass();
            //child.ToString();
            //Counter(1, 3, new Feedback(StaticClass.FeedbackToConsole));

            //Counter(1, 3, new Feedback(this.FeedbackToFile));
            //var st = new Student();
            //TestRef(ref st);
            ////st.test1(1);

            //Season t1 = Season.Autumn;
            //Enum.IsDefined(t1);

            //BaseClass.BaseStaticField = 20;
            //Console.WriteLine(DerivedClass.BaseStaticField);

            //var st = new Student();
            //var testClass = new Test();
            //Console.WriteLine("Max entries supported in list: "
            //                  + SomeLibraryType.MaxEntriesInList);

            //var set = new SetChild();
            //set.Find("test");
            //SetBase set1 = set;
            //set1.Find("test");
            //dynamic staticType = new StaticMemberDynamicWrapper(typeof(String));
            //Console.WriteLine(staticType.Concat("A", "B"));

            //byte b = 100;
            //var type1 = b.GetType();
            //var type2 = 100.GetType();
            //b += 200;
            //long greeting = 1213;
            //int a = greeting as int;


            //int[] array = { 1, 2, 3 };
            //Console.WriteLine("Hello\nWorld");
            //var names = new[] { "123","345",234};

            //String s1 = "Hello";
            //String s2 = "Hello";
            //Console.WriteLine(Object.ReferenceEquals(s1, s2));

            //var tuple = (x: 10, y: 20);
            //var type1 = tuple.GetType();
            //string[] values = new string[10];
            //string s = values[0];
            //Console.WriteLine(s.ToUpper());
        }

        static void TestRef(ref Student student)
        {

        }

        private static void Counter(Int32 from, Int32 to, Feedback fb)
        {
            for (Int32 val = from; val <= to; val++)
            {
                // If any callbacks are specified, call them
                if (fb != null)
                    fb(val);
            }
        }

        private void FeedbackToFile(Int32 value)
        {
            StreamWriter sw = new StreamWriter("Status", true);
            sw.WriteLine("Item=" + value);
            sw.Close();
        }
    }

    //class BaseClass
    //{
    //    public static int BaseStaticField = 10;
    //}

    //class DerivedClass : BaseClass
    //{
    //    // This class does not inherit or share the BaseStaticField
    //}
}
