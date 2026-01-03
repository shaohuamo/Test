using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject
{
    internal class HttpCookie
    {
        private readonly Dictionary<string,string> _cookies = new();

        public string this[string key]
        {
            get => _cookies[key];
            set => _cookies[key] = value;
        }
    }
}
