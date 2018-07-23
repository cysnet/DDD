using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Bbing.DtoModel.ResponseData;
using Bbing.Infrastructure;
using Bbing.WebApi.Filters;

namespace Bbing.WebApi.Controllers
{
    /// <summary>
    /// 工具
    /// </summary>
    public class ToolsController : BaseController
    {
        public static string PicUrl = "";
        static ToolsController() {
            PicUrl = ("PicUrl." + "Environment".GetAppSetting()).GetAppSetting();
        }
        /// <summary>
        /// 校验JWT
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public Dictionary<string, string> ValidateJwtToken(string token)
        {
            var reuslt = JwtHelper.ValidateJwtToken(token);
            return reuslt;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        [HttpPost,HttpAuth]
        public OutPutData FileUpload()
        {
            List<string> result = new List<string>();
            HttpFileCollection filelist = HttpContext.Current.Request.Files;
            if (filelist != null && filelist.Count > 0)
            {
                for (int i = 0; i < filelist.Count; i++)
                {
                    HttpPostedFile file = filelist[i];
                    String Tpath = "/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";
                    string FileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + file.FileName;
                    string FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/") + "Pic/UserHead" +  Tpath ;
                    DirectoryInfo di = new DirectoryInfo(FilePath);
                    if (!di.Exists) { di.Create(); }
                    file.SaveAs(FilePath + FileName);
                    result.Add(PicUrl + "Pic/UserHead" + "\\" + Tpath + "\\" + FileName);
                }
            }
            
            return Success("",result);
        }
    }
}
