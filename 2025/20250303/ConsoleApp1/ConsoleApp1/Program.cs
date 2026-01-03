using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp1
{
    internal class Program
    {
        public static void Main(int[] Args)
        {
            var tString = Args[0] + Args[1];
            Console.WriteLine(tString);
            //TaskFactory();
            //Thread.Sleep(100000);
        }

   
    }
}
