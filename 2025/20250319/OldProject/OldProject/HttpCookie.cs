using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldProject
{
    internal class HttpCookie
    {
        private readonly Dictionary<string, string> _cookies = new Dictionary<string, string>();

        public string this[string key]
        {
            get => _cookies[key];
            set => _cookies[key] = value;
        }
    }
}
