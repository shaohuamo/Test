using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ParentClass
    {
        public void Test()
        {
            var list = new List<int>();
            foreach (var i in list)
            {
                Console.WriteLine(i);
            }
        }
        public override string ToString()
        {
            Nullable<Int32> x = 5;
            Nullable<Int32> y = null;
            //var t = this.GetType();
            //var typeInfo = GetType();

            //var flag = typeInfo.IsDefined(typeof(FlagsAttribute), false);

            return base.ToString();
        }

        private static Object OneStatement(Stream stream, Char charToFind)
        {
            return (charToFind + ": " + stream.GetType() + String.Empty + (512M + stream.Position))
                .Where(c => c == charToFind).ToArray();
        }
    }

    public class ChildClass:ParentClass
    {
        public override string ToString()
        {
            var t = this.GetType();


            //var typeInfo = GetType();

            //var flag = typeInfo.IsDefined(typeof(FlagsAttribute), false);

            return base.ToString();
        }
    }
}
