using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asyncMethodLearning
{
    internal class AwaitPrinciple
    {
        static async Task Main1(string[] args)
        {
            Console.WriteLine($"Main start - Thread: {Thread.CurrentThread.ManagedThreadId}");

            await MakeRequestAsync();

            Console.WriteLine($"Main end - Thread: {Thread.CurrentThread.ManagedThreadId}");
        }

        static async Task MakeRequestAsync()
        {
            Console.WriteLine($"MakeRequestAsync start - Thread: {Thread.CurrentThread.ManagedThreadId}");

            using HttpClient client = new HttpClient();
            string result = await client.GetStringAsync("https://www.example.com");

            Console.WriteLine($"MakeRequestAsync end - Thread: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
