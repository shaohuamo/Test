using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread.ConditionVariablePattern
{
    /// <summary>
    /// 线程安全的队列,一次只有一个线程能访问到该队列
    /// 除非有了一个可供处理的item，否则试图出队列一个item的线程会一直阻塞
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SynchronizedQueue<T>
    {
        private readonly object _lock = new ();
        private readonly Queue<T> _queue = new();

        public void Enqueue(T item)
        {
            Monitor.Enter(_lock);

            _queue.Enqueue(item);
            //一个item如队列后，就可以唤醒 一个/所有 正在等待的线程
            Monitor.PulseAll(_lock);

            Monitor.Exit(_lock);
        }

        public T Dequeue()
        {
            Monitor.Enter(_lock);

            //队列为空，就一直循环
            while (_queue.Count==0)
            {
                Monitor.Wait(_lock);
            }
            
            //一个item出队列，并返回
            var item = _queue.Dequeue();
            Monitor.Exit(_lock);
            return item;
        }
    }
}
