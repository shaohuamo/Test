using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread
{
    /// <summary>
    /// 使用AutoResetEvent实现的递归锁
    /// </summary>
    public class RecursiveLockByAutoResetEvent:IDisposable
    {
        private readonly AutoResetEvent _autoResetEvent = new(true);
        private int _owningThreadId = 0;
        private int _recursionCount = 0;

        public void Enter()
        {
            //获取调用线程的ID
            var currentThreadId = Thread.CurrentThread.ManagedThreadId;

            //如果调用线程拥有锁，则递增递归计数
            if (_owningThreadId == currentThreadId)
            {
                //当该锁被ThreadA获取时，ThreadB无法通过if条件判断，
                //因此不会出现由于race condition导致的丢失更新问题，
                //因此这里不需要原子性操作的递增操作
                _recursionCount++;
                return;
            }

            //如果调用线程不拥有锁，阻塞调用线程
            _autoResetEvent.WaitOne();

            //调用线程拥有锁后，初始化拥有线程的ID和递归计数
            _owningThreadId = currentThreadId;
            _recursionCount = 1;
        }

        public void Exit()
        {
            //如果调用线程未拥有该锁，抛出异常
            if (_owningThreadId != Thread.CurrentThread.ManagedThreadId)
                throw new InvalidOperationException();

            //递归计数减一
            if (--_recursionCount ==0)
            {
                //如果递归计数为0，说明调用线程不再需要该锁
                _owningThreadId = 0;//重置拥有线程的ID
                _autoResetEvent.Set();//唤醒一个正在等待的线程(如果有的话)
            }
        }

        public void Dispose()
        {
            _autoResetEvent.Dispose();
        }
    }
}
