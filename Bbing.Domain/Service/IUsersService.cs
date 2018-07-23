using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bbing.Domain.Model;
using Bbing.Infrastructure.Redis;

namespace Bbing.Domain.Service
{
    public partial interface IUsersService
    {
        List<Users> GetUsers();
        object GetJwtToken(string userName, IRedisHelper redisHelper);
    }
}
