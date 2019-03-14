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

        public JsonResult RegeditUser(string userName, string passWord)
        {

        }
    }
}