using exiao.bll;
using exiao.model.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace exiao.com.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult Regedit()
        {

            return View();
        }

        public JsonResult RegeditUser(string userName, string password)
        {
            int id = B_User.Regedit(userName, password, "", "");
            Dto_AjaxReturnData<int> r = new Dto_AjaxReturnData<int>();
            r.code = AjaxResultCodeEnum.Success;
            r.data = id;
            return Json(r);
        }
    }
}