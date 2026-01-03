using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncMethodLearning
{
    public static class MyThreadPool
    {
        //BlockingCollection<T>内部使用了ConcurrentQueue<T>,
        //而ConcurrentQueue<T>实现了IProducerConsumerCollection<T>
        private static readonly BlockingCollection<(Action, ExecutionContext?)> s_workItems = new();

        //只能接受Action委托类型的参数
        public static void QueueUserWorkItem(Action action)
            => s_workItems.Add((action, ExecutionContext.Capture()));

        static MyThreadPool()
        {
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                new Thread(() =>
                    {
                        while (true)
                        {
                            //Take方法在s_workItems中元素个数为0时，会阻塞调用线程直至s_workItems中有可用的元素
                            (Action workItem, ExecutionContext? context) = s_workItems.Take();
                            if (context is null)
                            {
                                workItem();
                            }
                            else
                            {
                                //这种方式使用了Closure，会造成额外的内存内配，因此不使用这种方式
                                //因为编译器会为使用了Closure的lambda表达式生成独立的匿名类
                                //ExecutionContext.Run(context, delegate { workItem(); }, null);

                                //以下方法避免了Closure的使用，更加高效
                                ExecutionContext.Run(context, (object? state) => ((Action)state!).Invoke(), workItem);
                            }
                        }
                    })
                    { IsBackground = true }.Start();
            }
        }
    }
}
