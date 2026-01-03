using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread.Singleton
{
    /// <summary>
    /// 使用静态构造函数创建单实例对象
    /// 缺点：无法延迟Singleton对象的初始化
    /// </summary>
    public class Singleton2
    {
        //该字段引用一个单实例对象
        private static readonly Singleton2 s_value;

        static Singleton2()
        {
            s_value = new Singleton2();
        }

        //私有构造函数——防止在该类外部创建实例
        private Singleton2()
        {
            //初始化_value中的其他field(如果有的话)
        }

        public static Singleton2 GetSingleton() => s_value;
    }
}
