using exiao.sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace exiao.com
{
    public class LoginFilterAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// OnActionExecuting
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string DesUserModel = Util.GetCookie("exiao.user", "useridentity");
            if (string.IsNullOrEmpty(DesUserModel))
            {
                //string currentUrl = HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.ToString());
                //filterContext.Result = new RedirectResult("http://exiao.com/home/login?from=" + currentUrl);
                filterContext.Result = new RedirectResult("http://exiao.com/home/login");
            }
            base.OnActionExecuting(filterContext);

        }
    }
}