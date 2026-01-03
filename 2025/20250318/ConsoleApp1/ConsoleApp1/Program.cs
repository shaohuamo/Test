namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var t = args[0] + args[1];
            Console.WriteLine(t);
        }
    }

    struct Point()
    {
        public int m_x=1, m_y = 0;
    }
}
