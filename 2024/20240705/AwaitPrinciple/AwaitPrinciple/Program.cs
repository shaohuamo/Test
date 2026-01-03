namespace AwaitPrinciple
{
    internal class Program
    {
        private static AsyncLocal<string> asyncLocalVariable = new AsyncLocal<string>();

        static async Task Main(string[] args)
        {

            var downloading = DownloadDocsMainPageAsync();
            Console.WriteLine($"{nameof(Main)}: Launched downloading.");

            int bytesLoaded = await downloading;
            Console.WriteLine($"{nameof(Main)}: Downloaded {bytesLoaded} bytes.");
            Console.ReadLine();
            //asyncLocalVariable.Value = "Main Method Context";
            //Console.WriteLine($"Main: {asyncLocalVariable.Value}");

            //await Task.Run(async () =>
            //{
            //    asyncLocalVariable.Value = "Task 1 Context";
            //    Console.WriteLine($"Task 1 Start: {asyncLocalVariable.Value}");
            //    await Task.Delay(1000);
            //    Console.WriteLine($"Task 1 End: {asyncLocalVariable.Value}");
            //});

            //await Task.Run(async () =>
            //{
            //    asyncLocalVariable.Value = "Task 2 Context";
            //    Console.WriteLine($"Task 2 Start: {asyncLocalVariable.Value}");
            //    await Task.Delay(1000);
            //    Console.WriteLine($"Task 2 End: {asyncLocalVariable.Value}");
            //});

            //Console.WriteLine($"Main End: {asyncLocalVariable.Value}");
        }

        private static async Task<int> DownloadDocsMainPageAsync()
        {
            Console.WriteLine($"{nameof(DownloadDocsMainPageAsync)}: About to start downloading.");

            var client = new HttpClient();
            var content = await Content(client);

            Console.WriteLine($"{nameof(DownloadDocsMainPageAsync)}: Finished downloading.");
            return content.Length;
        }

        private static async Task<byte[]> Content(HttpClient client)
        {
            byte[] content = await client.GetByteArrayAsync("https://learn.microsoft.com/en-us/");
            return content;
        }

        //static async Task Main(string[] args)
        //{
        //    Console.WriteLine($"Main start - Thread: {Thread.CurrentThread.ManagedThreadId}");

        //    await MakeRequestAsync();

        //    Console.WriteLine($"Main end - Thread: {Thread.CurrentThread.ManagedThreadId}");
        //}

        //static async Task MakeRequestAsync()
        //{
        //    Console.WriteLine($"MakeRequestAsync start - Thread: {Thread.CurrentThread.ManagedThreadId}");

        //    using HttpClient client = new HttpClient();
        //    string result = await client.GetStringAsync("https://www.example.com");

        //    Console.WriteLine($"MakeRequestAsync end - Thread: {Thread.CurrentThread.ManagedThreadId}");
        //}
    }
}
