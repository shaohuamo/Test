using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread.Singleton
{
    /// <summary>
    /// 延迟Singleton对象的初始化
    /// 缺点：在内存中可能存在多个Singleton3对象
    /// 使用场合：追求性能，且创建Singleton3对象的代价较低
    /// </summary>
    public class Singleton3
    {
        //该字段引用一个单实例对象
        private static Singleton3? s_value;

        //私有构造函数——防止在该类外部创建实例
        private Singleton3()
        {
            //初始化_value中的其他field(如果有的话)
        }

        public static Singleton3 GetSingleton()
        {
            if (s_value !=null) return s_value;

            Singleton3 temp = new ();
            //使用Interlocked而不是Volatile的原因是：避免返回不同的Singleton实例
            //因为在上一行代码执行结束后，线程A可能中断，由另一个线程B创建Singleton实例B，
            //如果使用Volatile.Write方法，那么线程A会创建并返回Singleton实例A,
            //2个线程获取到的就是不同的Singleton实例
            Interlocked.CompareExchange(ref s_value, temp, null);
            return s_value;
        }
    }
}
