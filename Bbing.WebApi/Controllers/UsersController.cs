using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Bbing.Domain.BaseModel;
using Bbing.Domain.Model;
using Bbing.Domain.Service;
using Bbing.DtoModel.BaseData;
using Bbing.DtoModel.Consts;
using Bbing.DtoModel.Params.Users;
using Bbing.DtoModel.ResponseData;
using Bbing.Infrastructure;
using Bbing.Infrastructure.Redis;
using Bbing.WebApi.Filters;

namespace Bbing.WebApi.Controllers
{
    /// <summary>
    /// 用户操作
    /// </summary>
    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IRedisHelper redisHelper;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="usersService"></param>
        /// <param name="redisHelper"></param>
        public UsersController(IUsersService usersService, IRedisHelper redisHelper)
        {
            this.usersService = usersService;
            this.redisHelper = redisHelper;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="param">参数</param>
        [HttpPost, DataValidate]
        public OutPutData Regist([FromBody]RegistParam param)
        {
            if (this.usersService.CurrentRepository.Exist(e => e.UserName == param.UserName))
            {
                return Fail("用户名已存在");
            }
            var user = this.usersService.CurrentRepository.Insert(new Users()
            {
                UserName = param.UserName,
                Password = param.Password.ToMD5()
            });
            return Success("注册成功", usersService.GetJwtToken(user.UserName, redisHelper));
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="param"></param>
        [ HttpPost]
        public OutPutData Login([FromBody]LoginParam param)
        {
            var password = param.Password.ToMD5();
            if (this.usersService.CurrentRepository.Exist(e => e.UserName == param.UserName && e.Password == password))
            {
                return Success("登陆成功", usersService.GetJwtToken(param.UserName, redisHelper));
            }
            return Fail("用户名或密码不正确");
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="param"></param>
        [HttpPost,HttpAuth]
        public OutPutData ChangePwd([FromBody]ChangePwdParam param)
        {
            var password = param.OldPassword.ToMD5();
            if (!this.usersService.CurrentRepository.Exist(e => e.UserName ==Loginer.UserName && e.Password == password))
            {
                return Fail("密码不正确");
            }
            this.usersService.CurrentRepository.Update(e => e.UserName == Loginer.UserName,
                Param("Password", param.NewPassword.ToMD5()));
            return Success("修改成功");
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="param"></param>
        [HttpPost, HttpAuth]
        public OutPutData UploadHead([FromBody]UploadHeadParam param)
        {
            this.usersService.CurrentRepository.Update(e => e.UserName == Loginer.UserName,
                Param("HeadUrl", param.Url));
            return Success("修改成功");
        }
    }
}
