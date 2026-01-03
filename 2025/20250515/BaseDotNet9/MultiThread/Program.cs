using System.Collections.Concurrent;
using MultiThread.SimpleLock;

namespace MultiThread
{
    public delegate void MyDelegate(int a, int b);

    internal class Program
    {
        private static Timer s_timer;
        private static SimpleSpinLock _spinLock = new SimpleSpinLock();
        private MyDelegate? _myDelegate;

        private static readonly Lock _lockObj = new();
        private static readonly Object _Obj = new();
        public event MyDelegate? MyEvent
        {
            add
            {
                //使用interlocked anything pattern
                var actualOriginalValue = this._myDelegate;
                MyDelegate? expectedOriginalValue;
                do
                {
                    expectedOriginalValue = actualOriginalValue;
                    //desiredValue是新的实例
                    var desiredValue = (MyDelegate?)Delegate.Combine(expectedOriginalValue, value);
                    actualOriginalValue =
                        Interlocked.CompareExchange<MyDelegate>
                            (ref _myDelegate, desiredValue!, expectedOriginalValue);
                } while (actualOriginalValue != expectedOriginalValue);
            }
            remove
            {
                _myDelegate -= value;
            }
        }
        static void Main(string[] args)
        {
            //定时器
            s_timer = new Timer(Status, null, Timeout.Infinite, Timeout.Infinite);
            s_timer.Change(2000, Timeout.Infinite);



            //--------------------------------------------------------
            //生产者消费者模式
            //BlockingCollection类会将“非阻塞的集合”转换为“阻塞的集合”
            var bl = new BlockingCollection<int>(new ConcurrentQueue<int>());

            ThreadPool.QueueUserWorkItem(ConsumeItems, bl);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Producing: " +i);
                bl.Add(i);//在迭代过程中Add方法是安全的
            }

            //告诉消费者线程，不会在集合中添加更多的item了
            //也就是说告诉一个正在使用bl.GetConsumingEnumerable方法的Foreach循环：
            //当集合中没有可用的item时，不再阻塞调用线程，而是终止foreach循环
            bl.CompleteAdding();


            //-------------------------------------------------------------------
            //创建单实例对象
            //使用双检锁技术
            var s1 = new Lazy<string>(DateTime.Now.ToLongTimeString, LazyThreadSafetyMode.ExecutionAndPublication);
            //使用Interlocked.CompareExchange技术
            var s2 = new Lazy<string>(32.ToString, LazyThreadSafetyMode.PublicationOnly);

            Console.WriteLine(s1.IsValueCreated);//还没有查询Value,所以返回false
            Console.WriteLine(s1.Value);//延迟到使用时才创建string


            //-------------------------------------------------------------------
            //lock

            lock (_lockObj)
            {
                Console.WriteLine(1);
            }

            lock (_Obj)
            {
                Console.WriteLine(1);
            }



            //-------------------------------------------------------------------
            //interlocked
            int before = 9;
            var after = Interlocked.Increment(ref before);
            Console.WriteLine($"before value is {before}");
            Console.WriteLine($"after value is {after}");


            //-------------------------------------------------------------------
            //自定义spinlock
            _spinLock.Enter();
            //一次只有一个线程能够进入这里访问资源
            _spinLock.Exit();


            //-------------------------------------------------------------------
            //volatile
            //var sb = new VisibilityProblem();

            //var t1 = new Thread(sb.Worker);
            //var t2 = new Thread(sb.StopWorker);
            //t1.Start();
            //Thread.Sleep(5000);
            //t2.Start();

            //t1.Join();
            //t2.Join();

            Console.WriteLine("Hello, World!");
        }

        private static void Status(object? state)
        {
            //operations
            Thread.Sleep(1000);//模拟其他工作

            s_timer.Change(2000, Timeout.Infinite);
        }


        /// <summary>
        /// 异步的同步构造
        /// </summary>
        /// <param name="asyncLock">一般创建最大计数为1的SemaphoreSlim</param>
        /// <returns></returns>
        public static async Task AccessResourceViaAsyncSynchronization(SemaphoreSlim asyncLock)
        {
            //todo:执行你想要执行的任何代码

            //请求获得锁(这里可以将slot看锁，因为只有一个slot)
            await asyncLock.WaitAsync();//不会阻塞调用线程
            //执行到这里，表明没有其他线程在访问资源(获取到了唯一的slot)
            //todo:独占地访问资源

            //释放锁
            asyncLock.Release();

            //todo:执行你想要执行的任何代码
        }

        public static void ConsumeItems(object? o)
        {
            var bl = (BlockingCollection<int>)o!;
            //在迭代过程中会阻塞调用线程，直到出现一个可用的item
            foreach (var item in bl.GetConsumingEnumerable())
            {
                Console.WriteLine("Consuming: "+ item);
            }
            //集合空白，没有更多的item进入其中
            Console.WriteLine("All items have been consumed");
        }
    }
}
