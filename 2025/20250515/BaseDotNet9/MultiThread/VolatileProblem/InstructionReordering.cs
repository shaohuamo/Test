namespace MultiThread.VolatileProblem
{
    internal class InstructionReordering
    {
        private int _value = 0;
        private  volatile bool _flag = false;

        public void Thread1()
        {
            //以下代码在未使用volatile时，可能会按相反的顺序执行
            _value = 42;           // 步骤1

            //Volatile.Write(ref _flag,true);
            _flag = true;  // 步骤2 - volatile 保证不会重排序到步骤1之前
        }

        public void Thread2()
        {
            //在未使用volatile时，_value的值可能先于_flag读取

            //if (Volatile.Read(ref _flag))
            if (_flag)     // 如果看到 true，一定能看到 _value = 42
            {
                Console.WriteLine(_value);  // 保证输出 42
            }
        }
    }
}
