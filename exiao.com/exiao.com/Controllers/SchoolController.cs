using exiao.bll;
using exiao.model.dto;
using exiao.model.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace exiao.com.Controllers
{

    //[LoginFilterAttribute]
    public class SchoolController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.CurrentFunc = 1;
            return View();
        }

        public ActionResult AgentManage(int agentId)
        {
            ViewBag.CurrentFunc = 7;
            if (agentId != 0)
            {
                ViewBag.Agent = UserInfo.Agents.FirstOrDefault(a => a.Id == agentId);
            }
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

        public ActionResult ClassManage()
        {
            return View();
        }

        public ActionResult RoomManage()
        {
            return View();
        }

        public JsonResult GetAgentCourses(int agentId)
        {
            Dto_AjaxReturnData<T_Agent> result = new Dto_AjaxReturnData<T_Agent>();
            return Json(result);
        }

        public JsonResult AddAgent(string name, string shortName, string phone, string address)
        {
            //int UserId = 2;
            T_Agent a = B_Agent.AddAgent(name, shortName, phone, address, UserId);
            Dto_AjaxReturnData<T_Agent> result = new Dto_AjaxReturnData<T_Agent>();
            result.code = AjaxResultCodeEnum.Success;
            result.message = "";
            result.data = a;

            return Json(result);
        }
    }
}