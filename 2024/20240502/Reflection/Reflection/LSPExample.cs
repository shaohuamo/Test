using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class LSPExample
    {
    }

    public interface IGentleman
    {
        void Love();
    }

    public interface IKiller
    {
        void Kill();
    }

    public class WarmKiller:IGentleman,IKiller
    {
        public void Love()
        {
            Console.WriteLine("Let me kill the enemy..");
        }

        void IKiller.Kill()
        {
            Console.WriteLine("I will love you for ever");
        }
    }
}
