using exiao.bll;
using exiao.model.dto;
using exiao.model.entity;
using exiao.sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace exiao.com.Controllers
{
    public class BaseController : Controller
    {
        protected int UserId = 0;
        protected Dto_User UserInfo = null;

        public BaseController()
        {
            string DesUserModel = Util.GetCookie("exiao.user", "useridentity");
            string DesKey = Util.GetAppSetting("DesKey");
            UserCookieHelper.UserCookieModel u = UserCookieHelper.DescryptUserCookie(DesUserModel, DesKey);

            UserId = u._id;
            if (UserId != 0)
            {
                UserInfo = B_UserRedis.GetUser(UserId);
            }
            //ViewBag.UserInfo = UserInfo;
            ViewBag.Agents = UserInfo.Agents;
        }

        public void Logout()
        {
            Util.ClearCookies("exiao.user");
        }
    }
}