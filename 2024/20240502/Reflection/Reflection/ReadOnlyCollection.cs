using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class ReadOnlyCollection:IEnumerable
    {
        public int[] Array { get; set; }

        public ReadOnlyCollection(int[] array)
        {
            Array = array;
        }
        public IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public class Enumerator:IEnumerator
        {
            private readonly ReadOnlyCollection _collection;
            int _index;

            public Enumerator(ReadOnlyCollection collection)
            {
                _collection = collection;
                _index = -1;
            }
            public bool MoveNext()
            {
                if (_index<_collection.Array.Length-1)
                {
                    _index++;
                    return true;
                    
                }
                return false;
            }

            public void Reset()
            {
                _index=-1;
            }

            public object Current => _collection.Array[_index];
        }
    }
}
