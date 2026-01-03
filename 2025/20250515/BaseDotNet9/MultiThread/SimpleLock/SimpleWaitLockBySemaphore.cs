using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread.SimpleLock
{
    /// <summary>
    /// 允许多个线程并发只读访问共享资源
    /// </summary>
    public class SimpleWaitLockBySemaphore:IDisposable
    {
        private readonly Semaphore _semaphore;

        public SimpleWaitLockBySemaphore(int maxConcurrentCount)
        {
            _semaphore = new Semaphore(maxConcurrentCount, maxConcurrentCount);
        }

        public void Enter()
        {
            //如果没有可用的slot,则阻塞调用线程
            _semaphore.WaitOne();
        }

        public void Exit()
        {
            //释放一个slot
            _semaphore.Release();
        }

        public void Dispose()
        {
            _semaphore.Dispose();
        }
    }
}
