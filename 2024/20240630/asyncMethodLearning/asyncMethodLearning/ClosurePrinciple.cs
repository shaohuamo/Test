using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asyncMethodLearning
{
    internal class ClosurePrinciple
    {
        static void Main1()
        {
            Action action = CreateAction();
            //action();
            //action();
        }

        static Action CreateAction()
        {
            int counter = 0;
            return delegate
            {
                // Yes, it could be done in one statement; 
                // but it is clearer like this.
                counter++;
                Console.WriteLine("counter={0}", counter);
            };
        }
    }
}
