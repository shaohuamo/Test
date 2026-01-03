using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject
{
    internal class Publisher
    {
        public event EventHandler myEvent;

        public void RaiseEvent(EventArgs e)
        {
            //step 2: raise event
            //以线程安全的方式执行  委托对应的方法(引发event)
            var temp = Volatile.Read(ref myEvent);
            if (temp != null)
            {
                temp(this, e);
            }
        }
    }
}
