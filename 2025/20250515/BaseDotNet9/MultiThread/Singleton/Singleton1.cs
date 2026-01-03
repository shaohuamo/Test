using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MultiThread.Singleton
{
    /// <summary>
    /// 使用double check locking技术来创建单实例对象
    /// 适用场合：创建Singleton1对象的代码比较昂贵
    /// </summary>
    public class Singleton1
    {
        private static readonly object s_lock = new ();
        //该字段引用一个单实例对象
        private static Singleton1? s_value;

        //私有构造函数——防止在该类外部创建实例
        private Singleton1()
        {
            //初始化_value中的其他field(如果有的话)
        }

        public static Singleton1 GetSingleton()
        {
            if (s_value != null) return s_value;

            Monitor.Enter(s_lock);
            if (s_value == null)
            {
                //使用变量temp的目的是：
                //①调试方便:在调试时，可以在 volatile 写入之前检查临时变量的状态。
                //② 历史原因:早期的双重检查锁定模式文档和示例大多使用这种显式写法。
                Singleton1 temp = new ();
                //使用Volatile的原因是：
                //避免其他线程在第一次check时，获取到部分初始化的单实例对象
                Volatile.Write(ref s_value, temp);
            }
            Monitor.Exit(s_lock);

            return s_value;
        }
    }
}
