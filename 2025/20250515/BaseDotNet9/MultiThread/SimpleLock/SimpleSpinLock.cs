using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread.SimpleLock
{
    /// <summary>
    /// 简单的自旋互斥锁——在CPU上自旋
    /// </summary>
    internal struct SimpleSpinLock
    {
        private int _resourceInUse;//0=false,1=true

        public void Enter()
        {
            while (true)
            {
                if (Interlocked.Exchange(ref _resourceInUse,1)==0)
                {
                    return;
                }
            }
        }

        public void Exit()
        {
            //确保Enter和Exit之间的变量写入在Exit之前完成
            Volatile.Write(ref _resourceInUse,0);
        }
    }
}
