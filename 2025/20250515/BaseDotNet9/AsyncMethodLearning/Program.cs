using System.Threading.Tasks;

namespace AsyncMethodLearning
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //自定义Task中的WhenAll方法
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

            //-----------------------------------------------------------
            //自定义Task中的ContinueWith方法
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
            }).Wait();//wait方法会阻塞调用线程直到"How are you?"输出

            //--------------------------------------------------------------
            //模拟async和await的功能
            MyTask.Iterate(PrintAsync()).Wait();


            //-------------------------------------------------
            //自定义线程池
            AsyncLocal<int> myValue1 = new();
            for (int i = 0; i < 100; i++)
            {
                //Value属性的值更新时，会创建新的ExecutionContext实例，然后赋值给Thread.CurrentThread.ExecutionContext
                //所以在QueueUserWorkItem方法中每次捕捉到的都是不同的ExecutionContext实例
                myValue1.Value = i;
                MyThreadPool.QueueUserWorkItem(()=>
                {
                    Console.WriteLine(myValue1.Value);
                    Thread.Sleep(1000);
                });
            }

        }

        //迭代器
        static IEnumerable<MyTask> PrintAsync()
        {
            for (int i = 0; ; i++)
            {
                yield return MyTask.Delay(1000);
                Console.WriteLine(i);
            }
        }
    }
}
