using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread.SimpleLock
{
    public class SimpleWaitLockByAutoResetEvent:IDisposable
    {
        //true表示该锁可用
        private readonly AutoResetEvent _autoResetEvent = new(true);

        public void Enter()
        {
            //锁不可用时，阻塞调用线程
            //锁可用时，让调用线程通过，然后上锁
            _autoResetEvent.WaitOne();
        }

        public void Exit()
        {
            //开锁，唤醒阻塞线程之一，待其通过后，上锁
            //上锁操作是在WaitOne中实现的
            _autoResetEvent.Set();
        }

        public void Dispose()
        {
            _autoResetEvent.Dispose();
        }
    }
}
