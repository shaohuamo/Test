using System.Runtime.InteropServices;

namespace COM_Component_Example
{
    internal class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, int options);

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            MessageBox(IntPtr.Zero, "Hello, World!", "My COM Example", 0);
            //var test = new Test();
            //var s = test.ToString();
        }
    }

    public ref struct Test
    {
        public override string ToString()
        {
            return "";
        }
    }
}
