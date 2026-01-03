using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread.HybridLock
{
    /// <summary>
    /// 自定义的支持自旋、线程所有权和递归的混合互斥锁
    /// </summary>
    public class AnotherHybridLock:IDisposable
    {
        //正在等待该锁的线程数+获取到这个锁的线程数(1)
        //通过该字段来判断是通过用户模式构造还是内核模式构造来实现锁的获取和释放
        //获取锁时，该字段的值+1，释放锁时，该字段的值-1
        private int _waitAndGetLockThreadCount;
        //AutoResetEvent是基元内核模式构造
        private readonly AutoResetEvent _autoResetEvent = new(false);
        //自旋次数
        private const int SpinCount = 4000;
        //获取该锁的线程的threadId和获取次数
        private int _owningThreadId = 0;
        private int _recursionCount = 0;

        public void Enter()
        {
            //如果调用线程已经获取锁，则递归计数+1
            int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            if (_owningThreadId == currentThreadId)
            {
                _recursionCount++;
                return;
            }

            //调用线程未获取锁，尝试获取它(_waitAndGetLockThreadCount的值会+1)
            SpinWait spinWait = new ();

            //尝试使用"用户模式构造"来实现锁的获取
            //在自旋的过程中，如果无竞争，则获取锁
            for (int i = 0; i < SpinCount; i++)
            {
                //使用CompareExchange而不是Increment的原因：在Loop中
                if (Interlocked.CompareExchange(ref _waitAndGetLockThreadCount,1,0)==0)
                {
                    //使用goto是为了避免代码重复和提升性能关键路径代码的性能
                    goto GotLock;
                }
                
                spinWait.SpinOnce();//执行一次时间极短的自旋
            }

            //原因：在自旋结束后 和 进入阻塞等待之间，可能正好有其他线程释放了锁！

            //自旋结束后，再尝试一次使用"用户模式构造"来实现锁的获取
            //更新前_waitAndGetLockThreadCount=0时，表示没有线程正在使用该锁(无竞争)
            //更新前_waitAndGetLockThreadCount>=1时，表明该锁正在被某个线程使用(有竞争)
            //在阻塞调用线程前，让_waitAndGetLockThreadCount自增1，表明有一个线程要被阻塞了
            if (Interlocked.Increment(ref _waitAndGetLockThreadCount) >1)
            {
                //使用"内核模式构造"来实现锁的获取
                _autoResetEvent.WaitOne();
            }

            //获取锁之后的操作
            GotLock:
            //一个线程获得锁后，记录该线程的ID，并指出该线程获取锁一次
            _owningThreadId = currentThreadId;
            _recursionCount = 1;
        }

        public void Exit()
        {
            //如果调用线程没有获取到该锁，抛出异常
            if (_owningThreadId != Thread.CurrentThread.ManagedThreadId)
                throw new InvalidOperationException();

            //递归计数-1后，若_recursionCount>0说明调用线程仍然拥有该锁
            if (--_recursionCount > 0) return;

            //此时调用线程不再需要该锁
            _owningThreadId = 0;//在释放锁之前重置owningThreadId
            //尝试使用"用户模式构造"来实现锁的释放
            //若没有其他线程在等待该锁，直接return
            if (Interlocked.Decrement(ref _waitAndGetLockThreadCount) ==0) return;

            //使用"内核模式构造"来实现锁的释放
            //有其他线程在等待该锁，唤醒其中一个
            _autoResetEvent.Set();//这里有较大的性能损失
        }

        public void Dispose()
        {
            _autoResetEvent.Dispose();
        }
    }
}
