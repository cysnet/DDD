using Bbing.Domain.Model;
using Bbing.Infrastructure.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bbing.Infrastructure;
using Bbing.DtoModel.Consts;

namespace Bbing.Service.Services.Mongo
{
    public partial class UsersService
    {
        public List<Users> GetUsers()
        {
            var users = this.CurrentRepository.GetMany(e => true);
            return users;
        }

        /// <summary>
        /// 获取登陆jwttoken
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="redisHelper"></param>
        /// <returns></returns>
        public object GetJwtToken(string userName, IRedisHelper redisHelper)
        {
            var token = JwtHelper.GetJwtToken(new Dictionary<string, string>() { { "UserName", userName }, { "RandomNum", Guid.NewGuid().ToString() } });
            var userInfo = CurrentRepository.GetOne(e => e.UserName == userName, e => new { e.HeadUrl });
            redisHelper.Set(ConstData.UserLoginJwt + userName, token, TimeSpan.FromDays(5));
            return new { userInfo,token};
        }
    }
}
