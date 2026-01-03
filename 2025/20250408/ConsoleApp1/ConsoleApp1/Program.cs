namespace HelloWorld
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            Hello("Hello World!");
        }

        static partial void Hello(string name);
    }
}
