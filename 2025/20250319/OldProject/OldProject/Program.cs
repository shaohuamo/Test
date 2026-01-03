using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OldProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Point myPoint = new Point(5, 5);
            myPoint.Print();  // Output: Point(5, 5)

            ModifyPoint(ref myPoint);
            myPoint.Print();  // Output: Point(15, 15) - modified
        }

        static void ModifyPoint(ref Point p)
        {
            // Reassign a new instance
            p = new Point(p.X + 10, p.Y + 10);
        }

    }

    struct Point
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

    //struct Point
    //{
    //    //public int x, y;

    //    //public Point(int m)
    //    //{
    //    //    y=x = m;
    //    //    this = new Point();
    //    //}
    //}

    class MyClass
    {
        public MyClass()
        {
            //this = new MyClass();
        }
    }
}
