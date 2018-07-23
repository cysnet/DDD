using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Bbing.DtoModel.Consts;
using Bbing.DtoModel.ResponseData;
using Bbing.Infrastructure;
using Bbing.Infrastructure.Redis;

namespace Bbing.WebApi.Filters
{
    public class HttpAuthAttribute : ActionFilterAttribute
    {
        public HttpAuthAttribute()
        {
            this.redisHelper = new CSRedisHelper();
        }
        private IRedisHelper redisHelper;
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            string token = "";
            var request = ((HttpContextBase)filterContext.Request.Properties["MS_HttpContext"]).Request;
            token = request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                token = request.Params["token"];
            }
            if (string.IsNullOrEmpty(token))
            {
                filterContext.Response = filterContext.Request.CreateResponse( OutPutData.NewOutPutData("请授权", false));
                return;
            }
            var JwtDictionary = JwtHelper.ValidateJwtToken(token);
            if (JwtDictionary == null)
            {
                filterContext.Response = filterContext.Request.CreateResponse( OutPutData.NewOutPutData("非法授权", false));
                return;
            }
            if (string.IsNullOrEmpty(JwtDictionary["UserName"]) || string.IsNullOrEmpty(JwtDictionary["RandomNum"]))
            {
                filterContext.Response = filterContext.Request.CreateResponse( OutPutData.NewOutPutData("非法授权", false));
                return;
            }
            if (!redisHelper.Exist(ConstData.UserLoginJwt + JwtDictionary["UserName"]))
            {
                filterContext.Response = filterContext.Request.CreateResponse(OutPutData.NewOutPutData("请重新登陆", false));
                return;
            }
            var RedisJwtDictionary = JwtHelper.ValidateJwtToken(redisHelper.Get(ConstData.UserLoginJwt + JwtDictionary["UserName"]));
            if (RedisJwtDictionary["RandomNum"] != JwtDictionary["RandomNum"])
            {
                filterContext.Response = filterContext.Request.CreateResponse( OutPutData.NewOutPutData("您的账号已在其他地方登陆", false));
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}