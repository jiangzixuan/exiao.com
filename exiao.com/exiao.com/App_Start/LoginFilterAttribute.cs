﻿using exiao.sdk;
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
                //if (HttpContext.Current.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                //{
                //    //ajax 请返回jsonresult
                //    JsonResult json = new JsonResult();
                //    json.Data = new { status = "100", message = "请先登录再使用此功能！", value = 0 };
                //    json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                //    filterContext.Result = json;
                //}
                //else
                //{
                //    filterContext.Result = new RedirectResult("http://user.easyzy.com?from=" + filterContext.HttpContext.Request.Url);
                //}
                string currentUrl = HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.ToString());
                filterContext.Result = new RedirectResult("http://exiao.com/home/login?from=" + currentUrl);
            }
            base.OnActionExecuting(filterContext);

        }
    }
}