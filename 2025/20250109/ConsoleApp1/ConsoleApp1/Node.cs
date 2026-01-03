using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Node
    {
    }

    internal sealed class TypeNode<T> : Node
    {
        public T m_data;
        public Node m_next;

        public TypeNode(T data,Node next)
        {
            m_data=data;
            m_next=next;
        }
    }

}
