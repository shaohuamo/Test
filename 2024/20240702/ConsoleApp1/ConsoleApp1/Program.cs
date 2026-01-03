using System.Reflection.Metadata.Ecma335;

namespace ConsoleApp1
{
    public  class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("hello,");
            Task.Delay(2000).ContinueWith(task => Console.WriteLine("world"));
            Console.WriteLine("main thread done!");
            Console.ReadLine();

            await GreetingAsync("");
            //e TestAsync();
            //Console.WriteLine("Hello, World!");
        }

        public static Task TestAsync()
        {
            Console.WriteLine("test was executed");
            return Task.CompletedTask;
        }

        public static Task<string> GreetingAsync(string name) =>
            Task.Run<string>(() => "");
    }
}
