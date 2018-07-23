using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Bbing.Domain.BaseModel;
using Bbing.DtoModel.BaseData;
using Bbing.DtoModel.Consts;
using Bbing.DtoModel.ResponseData;
using Bbing.Infrastructure;
using Bbing.Infrastructure.Redis;

namespace Bbing.WebApi.Controllers
{
    /// <summary>
    /// 基控制器
    /// </summary>
    public class BaseController : ApiController
    {

        private Loginer CurrentLoginer;
        /// <summary>
        /// 当前登陆人
        /// </summary>
        public Loginer Loginer
        {
            set
            {
                CurrentLoginer = value;
            }
            get
            {
                if (CurrentLoginer == null)
                {
                    var request = ((HttpContextBase)Request.Properties["MS_HttpContext"]).Request;
                    var token = request.Headers["token"];
                    var JwtDictionary = JwtHelper.ValidateJwtToken(token);
                    if (JwtDictionary == null)
                    {
                        return null;
                    }
                    if (string.IsNullOrEmpty(JwtDictionary["UserName"]) || string.IsNullOrEmpty(JwtDictionary["RandomNum"]))
                    {
                        return null;
                    }
                    CurrentLoginer = new Loginer(JwtDictionary["UserName"]);
                    return new Loginer(JwtDictionary["UserName"]);
                }
                return CurrentLoginer;
            }
        }

        /// <summary>
        /// 返回失败结果
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public OutPutData Fail(string message="失败", object data = null)
        {
            return OutPutData.NewOutPutData(message, false, data);
        }

        /// <summary>
        /// 返回成功结果
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public OutPutData Success(string message="成功", object data = null)
        {
            return OutPutData.NewOutPutData(message, true, data);
        }

        /// <summary>
        /// 获取KV
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public KV Param(string key,string value)
        {
            return new KV(key,value);
        }
    }
}
