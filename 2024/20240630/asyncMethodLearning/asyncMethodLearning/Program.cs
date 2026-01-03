using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

// ------------------------------------------------------------
AsyncLocal<int> myValue = new();
List<MyTask> tasks = new();
for (int i = 0; i < 100; i++)
{
    myValue.Value = i;
    tasks.Add(MyTask.Run(delegate
    {
        Console.WriteLine(myValue.Value);
        Thread.Sleep(1000);
    }));
}
MyTask.WhenAll(tasks).Wait();

// ------------------------------------------------------------
Console.Write("Hello1, ");
MyTask.Delay(2000).ContinueWith(delegate
{
    Console.Write("World1! ");
}).Wait();


Console.Write("Hello2, ");
//第一个ContinueWith方法的参数是Func<MyTask>,第二个ContinueWith方法的参数是Action
MyTask.Delay(2000).ContinueWith(delegate
{
    Console.Write("World2! ");
    return MyTask.Delay(2000).ContinueWith(delegate
    {
        Console.WriteLine("How are you?");
    });
}).Wait();

//异步迭代器
// ------------------------------------------------------------
for(int i=0;;i++)
{
	/*await在这里不会生效*/ MyTask.Delay(1000);
	Console.Write(i);
}


MyTask.Iterate(PrintAsync()).Wait();
//异步迭代器
static IEnumerable<MyTask> PrintAsync()
{
    for (int i = 0; ; i++)
    {
        yield return MyTask.Delay(1000);
        Console.WriteLine(i);
    }
}

// ------------------------------------------------------------
class MyTask
{

    private bool _completed;
    private Exception? _exception;
    private Action? _continuation;
    private ExecutionContext? _context;

    public struct Awaiter(MyTask t) : INotifyCompletion
    {
        public Awaiter GetAwaiter() => this;
        public bool IsCompleted => t.IsCompleted;
        public void OnCompleted(Action continuation) => t.ContinueWith(continuation);
        public void GetResult() => t.Wait();
    }

    public Awaiter GetAwaiter() => new(this);

    public bool IsCompleted
    {
        get
        {
			//使用线程同步的原因是：线程池线程和main线程有可能会同时访问这个_completed字段
			//在实际的Task中会尽可能使用无锁编程
            lock (this)
            {
                return _completed;
            }
        }
    }

	//标记Task“完成且成功”
    public void SetResult() => Complete(null);
	
	//标记Task“完成但有异常”
    public void SetException(Exception exception) => Complete(exception);

    private void Complete(Exception? exception)
    {
        lock (this)
        {
			//如果Task已经被标记过了，则抛出异常
            if (_completed) throw new InvalidOperationException("Stop messing up my code");

            _completed = true;
            _exception = exception;

            if (_continuation is not null)
            {
                MyThreadPool.QueueUserWorkItem(delegate
                {
                    if (_context is null)
                    {
                        _continuation();
                    }
                    else
                    {
                        ExecutionContext.Run(_context, (object? state) => ((Action)state!).Invoke(), _continuation);
                    }
                });
            }
        }
    }

    public void Wait()
    {
        ManualResetEventSlim? mres = null;

        lock (this)
        {
            if (!_completed)
            {
				//只有在Task未完成时，才需要阻塞Wait方法，即才需要ManualResetEventSlim对象
                mres = new ManualResetEventSlim();
				//Monitor锁是可重入的——ContinueWith方法内部会重新获取锁
				//当Task执行结束后，调用callback，发出信号，唤醒等待的main线程
                ContinueWith(mres.Set);
            }
        }
		//调用线程(main线程)会被阻塞
        mres?.Wait();

        if (_exception is not null)
        {
			//这种方式抛出一个新的异常，且传递了ex作为内部异常，不会导致stack跟踪信息丢失
			//之所以返回AggregateException而不是Exception，是因为Task中可能包含多个Task,例如Task.WhenAll返回的Task对象
			//throw new AggregateException(_exception);
			
			//用于跨上下文（如异步、存储后）重新抛出之前保存的异常,且保持原始堆栈
            ExceptionDispatchInfo.Throw(_exception);
        }
    }

    public MyTask ContinueWith(Action action)
    {
        MyTask t = new();

        Action callback = () =>
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                t.SetException(e);
                return;
            }

            t.SetResult();
        };

        lock (this)
        {
            if (_completed)
            {
				//将callback托管到线程池线程上的原因是：避免在锁内执行用户代码，防止锁持有时间过长
				//MyThreadPool.QueueUserWorkItem方法内部会捕捉到调用线程(main)线程的ExecutionContext
                MyThreadPool.QueueUserWorkItem(callback);
            }
            else
            {
                _continuation = callback;
				//ContinueWith方法是在main线程中执行的，因此这里捕捉到的是main线程的ExecutionContext
                _context = ExecutionContext.Capture();
            }
        }

        return t;
    }

    public MyTask ContinueWith(Func<MyTask> action)
    {
        MyTask t = new();

        Action callback = () =>
        {
            try
            {
                MyTask next = action();
				//next.ContinueWith方法的目的是：t需要等待action完成之后，才能将t标记为完成
                next.ContinueWith(delegate
                {
					//在next表示的Task结束之后，如果next中发生了异常A，则将A保存到t中
                    if (next._exception is not null)
                    {
                        t.SetException(next._exception);
                    }
                    else
                    {
                        t.SetResult();
                    }
                });
            }
            catch (Exception e)
            {
                t.SetException(e);
                return;
            }
        };

        lock (this)
        {
            if (_completed)
            {
                MyThreadPool.QueueUserWorkItem(callback);
            }
            else
            {
                _continuation = callback;
                _context = ExecutionContext.Capture();
            }
        }

        return t;
    }

    public static MyTask Run(Action action)
    {
        MyTask t = new();

        MyThreadPool.QueueUserWorkItem(() =>
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                t.SetException(e);
                return;
            }

            t.SetResult();
        });

        return t;
    }

    public static MyTask WhenAll(List<MyTask> tasks)
    {
        MyTask t = new();

        if (tasks.Count == 0)
        {
            t.SetResult();
        }
        else
        {
            int remaining = tasks.Count;

            Action continuation = () =>
            {
				//每个Task完成后remaining-1
                if (Interlocked.Decrement(ref remaining) == 0)
                {
                    // TODO: handle exceptions
					//若remaining-1的值为0，说明最后一个Task完成
                    t.SetResult();
                }
            };

            foreach (var task in tasks)
            {
                task.ContinueWith(continuation);
            }
        }

        return t;
    }

	//await Task.Delay(1000);可以实现在pause logical flow的时候，让线程去做其他的事情
    public static MyTask Delay(int timeout)
    {
        MyTask t = new();
		//new Timer(_ => t.SetResult())会创建一个不会立即执行的Timer
		//Change方法修改Timer执行前的等待时间
		
		//该Timer的功能是：在timeout时间后，将t标记为“完成且成功”
        new Timer(_ => t.SetResult()).Change(timeout, -1);
        return t;
    }

	//用foreach代码中对迭代器的处理的方式模拟了async关键字生成的state machine
    public static MyTask Iterate(IEnumerable<MyTask> tasks)
    {
        MyTask t = new();

        IEnumerator<MyTask> e = tasks.GetEnumerator(); // TODO: dispose

		//local method
        void MoveNext()
        {
            try
            {
                while (e.MoveNext())
                {
                    MyTask next = e.Current;
					//如果next表示的Task同步完成了，就不需要使用ContinueWith方法异步的调用MoveNext方法了
                    if (next.IsCompleted)
                    {
                        next.Wait();
                        continue;
                    }

                    next.ContinueWith(MoveNext);
                    return;
                }
            }
            catch (Exception ex)
            {
                t.SetException(ex);
                return;
            }

			//最后一个Task结束时，才会执行
            t.SetResult();
        }

        MoveNext();

        return t;
    }
}

static class MyThreadPool
{
    private static readonly BlockingCollection<(Action, ExecutionContext?)> s_workItems = new();

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
                    (Action workItem, ExecutionContext? context) = s_workItems.Take();
                    if (context is null)
                    {
                        workItem();
                    }
                    else
                    {
                        //这种方式使用了Closure，会造成额外的内存内配，因此不使用这种方式
                        //ExecutionContext.Run(context, delegate { workItem();},null);

                        //以下方法避免了Closure的使用，更加高效
                        ExecutionContext.Run(context, (object? state) => ((Action)state!).Invoke(), workItem);
                    }
                }
            })
            { IsBackground = true }.Start();
        }
    }
}