namespace MultiThread.VolatileProblem
{
    public class VisibilityProblem
    {
        //volatile关键字告诉C#和JIT编译器不将字段缓存到CPU寄存器中，
        // 确保字段的所有读写操作都是内存中进行
        private volatile bool _stopWorker;

        public void Worker()
        {
            int x = 0;
            while (!_stopWorker)// 每次都会从内存重新读取
            {
                x++;
            }
            Console.WriteLine($"Worker:stopped when x={x}");
        }

        public void StopWorker()
        {
            _stopWorker = true;
        }
    }
}
