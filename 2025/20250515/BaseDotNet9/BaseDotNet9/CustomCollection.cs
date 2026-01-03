using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDotNet9
{
    internal class CustomCollection
    {
        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return i;
            }
        }

        public IEnumerable<int> Reserve()
        {
            for (int i = 10; i > 0; i--)
            {
                yield return i;
            }
        }
    }
}
