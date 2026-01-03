using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread.ConditionVariablePattern
{
    /// <summary>
    /// 使用“条件变量”模式，让一个线程在一个复合条件为true时，执行一些代码
    /// </summary>
    public class ConditionVariablePattern
    {
        private readonly object _lock = new();
        private bool _condition = false;

        public void Thread1()
        {
            Monitor.Enter(_lock);//获取一个互斥锁

            //在锁中，"原子性"地测试复合条件——只在同步代码块中访问复合条件
            while (!_condition)
            {
                //wait方法会阻塞调用线程
                Monitor.Wait(_lock);//临时释放锁，使其他线程可以获取到该锁
                //被唤醒后，会重新获取了锁，且从这里继续执行
            }
            //条件满足，处理数据...
            Monitor.Exit(_lock);//永久释放锁
        }

        public void Thread2()
        {
            Monitor.Enter(_lock);//获取一个互斥锁

            //处理数据并修改条件
            _condition=true;

            //Monitor.Pulse(_lock);//锁释放后，唤醒一个正在等待的线程

            //锁释放后，唤醒所有正在等待的线程，让它们竞争，
            //成功的线程获取锁，失败的线程继续阻塞
            Monitor.PulseAll(_lock);

            Monitor.Exit(_lock);//释放锁

        }
    }
}
