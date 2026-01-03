using System.Globalization;
using System.Numerics;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int[] array = new int[4] { 1, 2, 3, 4 };
            Array.Sort(array);



            double result0 = Math.Log2(15);
            double result = Math.Log2(16);
            double result3 = Math.Log2(18);

            int result01 = BitOperations.Log2(15);  // Returns 4
            int result11 = BitOperations.Log2(16);  // Returns 4
            int result02 = BitOperations.Log2(20); // Returns 4 (since 2⁴ = 16 < 20 < 32 = 2⁵)




            string test = "oweoiweb";
            Console.WriteLine(test.GetHashCode());
            Console.WriteLine(test.GetHashCode());

            //var names = Enum.GetNames<Color>();
            //var value1 = Enum.GetValues(typeof(Color));
            //var value2 = Enum.GetValues<Color>();



            //var list = new List<int>(10);


            //DateTime start = DateTime.UtcNow;
            //PrintDateTime(start);
            //PrintDateTime(in start);

            //PrintDateTime(start.AddMinutes(1));

            //PrintDateTime(in start.AddMinutes(1));

            //Point myPoint = new Point(5, 5);
            //myPoint.Print();  // Output: Point(5, 5)

            //ModifyPoint(in myPoint);
            //myPoint.Print();  // Output: Point(15, 15) - modified
        }

        static void ModifyPoint(in Point p)
        {
            // Reassign a new instance
            Console.WriteLine(p.X);

        }

        static IEnumerable<int> GetNumbers(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                yield return i; // Returns each number one at a time
            }
        }


        static void PrintDateTime(in DateTime value)
        {
            string text = value.ToString(
                "yyyy-MM-dd'T'HH:mm:ss",
                CultureInfo.InvariantCulture);
            Console.WriteLine(text);
        }
    }

    public class Point
    {


        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }



        public void Print() => Console.WriteLine($"Point({X}, {Y})");
    }

    public enum Color
    {
        Red = 1,
        Green = 2,
        Blue = 3
    }
}
