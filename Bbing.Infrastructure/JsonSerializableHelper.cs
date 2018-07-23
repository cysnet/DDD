using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bbing.Infrastructure
{
    public static class JsonSerializableHelper
    {
        public static string ObjToString(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T StringToObj<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
