using Bbing.DtoModel.ResponseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace Bbing.WebApi.Filters
{
    public class DataValidateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {

            string mes = "";
            if (!filterContext.ModelState.IsValid)
            {
                StringBuilder errinfo = new StringBuilder();
                foreach (var s in filterContext.ModelState.Values)
                {
                    foreach (var p in s.Errors)
                    {
                        errinfo.AppendFormat("{0},", p.ErrorMessage);
                    }
                }
                mes = errinfo.ToString().TrimEnd(',');
                filterContext.Response = filterContext.Request.CreateResponse( OutPutData.NewOutPutData(mes, false));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}