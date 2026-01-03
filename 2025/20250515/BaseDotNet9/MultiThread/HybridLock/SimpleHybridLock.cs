using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread.HybridLock
{
    /// <summary>
    /// 自定义的混合锁(用户模式构造+内核模式构造)
    /// </summary>
    public class SimpleHybridLock : IDisposable
    {
        //正在等待该锁的线程数+获取到这个锁的线程数(1)
        private int _waitAndGetLockThreadCount;

        //AutoResetEvent是基元内核模式构造
        private readonly AutoResetEvent _autoResetEvent = new(false);

        public void Enter()
        {
            //(无竞争时)尝试使用"用户模式构造"来实现锁的获取
            if (Interlocked.Increment(ref _waitAndGetLockThreadCount) == 1) return;

            //(有竞争时)使用"内核模式构造"来实现锁的获取
            _autoResetEvent.WaitOne();//WaitOne返回后，调用线程获取该锁
        }

        public void Exit()
        {
            //(无竞争时)尝试使用"用户模式构造"来实现锁的释放
            if (Interlocked.Decrement(ref _waitAndGetLockThreadCount) ==0) return;

            //(有竞争时)使用"内核模式构造"来实现锁的释放
            _autoResetEvent.Set();//唤醒等待的线程之一
        }

        public void Dispose()
        {
            _autoResetEvent.Dispose();
        }
    }
}
