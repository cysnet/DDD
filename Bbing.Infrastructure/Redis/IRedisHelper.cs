using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbing.Infrastructure.Redis
{
    public interface IRedisHelper
    {
        void Set(string key, string value);

        void Set<T>(string key, T value);

        string Get(string key);

        T Get<T>(string key);
        void Set(string key, string value, TimeSpan span);
        void Set<T>(string key, T value, TimeSpan span);

        bool Exist(string key);
    }
}
