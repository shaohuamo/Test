using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyStroller.SDK
{
    //让第三方用来标记插件中未完成的Animal Class
    //以便主体程序中bypass未完成的Animal Class
    public class UnfinishedAttribute:Attribute
    {
    }
}
