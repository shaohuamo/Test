namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            IList<int> list = new List<int>();
            var array = new int[] { 1, 2 };
            Test(list);
            Test(array);

        }

        private static void Test(IList<int> list)
        {
            list.Add(10);
        }
    }
}
