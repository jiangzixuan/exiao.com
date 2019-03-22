using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace exiao.com.Controllers
{

    [LoginFilterAttribute]
    public class SchoolController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgentManage()
        {
            return View();
        }

        public ActionResult TeacherManage()
        {
            return View();
        }

        public ActionResult StudentManage()
        {
            return View();
        }
    }
}