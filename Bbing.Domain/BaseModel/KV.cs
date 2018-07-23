using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.Domain.BaseModel
{
    public class KV
    {
        public string key { get; set; }
        public object value { get; set; }
        public KV(string _key, object _value) { key = _key; value = _value; }
        public KV() { }
    }
}
