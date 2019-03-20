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
            //打开获取一个token，作为redis存放验证码的key
            string Token = UniqueObjectID.GenerateStrNewId();
            ViewBag.Token = Token;
            return View();
        }

        /// <summary>
        /// 显示验证码，并记录redis
        /// </summary>
        /// <param name="token"></param>
        public void ShowCheckCode(string token)
        {
            string checkCode = GenerateCheckCode();
            CreateCheckCodeImage(checkCode);
            B_CheckCodeRedis.SetCheckCode(token, checkCode);
        }

        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public JsonResult IsUserNameExists(string userName)
        {
            Dto_AjaxReturnData<bool> result = new Dto_AjaxReturnData<bool>();
            bool s = B_User.IsUserNameExists(userName);
            if (s)
            {
                result.code = AjaxResultCodeEnum.Success;
                result.message = "";
                result.data = true;
                return Json(result);
            }
            else
            {
                result.code = AjaxResultCodeEnum.Error;
                result.message = "";
                result.data = false;
                return Json(result);
            }
        }

        /// <summary>
        /// 验证码是否正确
        /// </summary>
        /// <param name="token"></param>
        /// <param name="checkCode"></param>
        /// <returns></returns>
        public JsonResult IsCheckCodeCorrect(string token, string checkCode)
        {
            Dto_AjaxReturnData<bool> result = new Dto_AjaxReturnData<bool>();
            string CorrectCode = B_CheckCodeRedis.GetCheckCode(token);
            if (CorrectCode.ToLower() == checkCode.ToLower())
            {
                result.code = AjaxResultCodeEnum.Success;
                result.message = "";
                result.data = true;
            }
            else
            {
                result.code = AjaxResultCodeEnum.Error;
                result.message = "";
                result.data = false;
            }
            return Json(result);
        }
        

        public JsonResult RegeditUser(string userName, string password, string token, string checkCode)
        {
            string CorrectCode = B_CheckCodeRedis.GetCheckCode(token);
            Dto_AjaxReturnData<int> result = new Dto_AjaxReturnData<int>();

            if (checkCode.ToLower() != CorrectCode.ToLower())
            {
                result.code = AjaxResultCodeEnum.Error;
                result.message = "验证码错误";
                result.data = 0;
                return Json(result);
            }
            if (B_User.IsUserNameExists(userName))
            {
                result.code = AjaxResultCodeEnum.Error;
                result.message = "用户名已被使用";
                result.data = 0;
                return Json(result);
            }

            int id = B_User.Regedit(userName, password, "", "");

            result.code = AjaxResultCodeEnum.Success;
            result.data = id;
            return Json(result);
        }

        public JsonResult LoginUser(string userName, string password, string isAutoLogin)
        {
            Dto_AjaxReturnData<int> result = new Dto_AjaxReturnData<int>();
            T_User u = B_User.Login(userName, password);
            if (u != null)
            {
                B_User.LoginCookie(u, isAutoLogin == "0" ? false : true);
                result.code = AjaxResultCodeEnum.Success;
                result.message = "";
                result.data = u.Id;
            }
            else
            {
                result.code = AjaxResultCodeEnum.Error;
                result.message = "";
                result.data = 0;
            }
            return Json(result);
        }

        #region 验证码相关
        /// <summary>
        /// 生成长度为4的验证码
        /// </summary>
        /// <returns></returns>
        private string GenerateCheckCode()
        {
            char code;
            string checkCode = String.Empty;
            int validateCodeCount = 4;
            // 验证码的字符集，去掉了一些容易混淆的字符
            char[] character = { '2', '3', '4', '5', '6', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            System.Random random = new Random();

            for (int i = 0; i < validateCodeCount; i++)
            {
                code = character[random.Next(character.Length)];

                checkCode += code;
            }
            return checkCode;
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="checkCode"></param>
        private void CreateCheckCodeImage(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return;

            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 15.0 + 40)), 23);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);

            try
            {
                //生成随机生成器  
                Random random = new Random();

                //清空图片背景色  
                g.Clear(System.Drawing.Color.White);

                //画图片的背景噪音线  
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Silver), x1, y1, x2, y2);
                }

                System.Drawing.Font font = new System.Drawing.Font("Arial", 14, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new System.Drawing.Rectangle(0, 0, image.Width, image.Height), System.Drawing.Color.Blue, System.Drawing.Color.DarkRed, 1.2f, true);

                int cySpace = 16;
                for (int i = 0; i < 4; i++)
                {
                    g.DrawString(checkCode.Substring(i, 1), font, brush, (i + 1) * cySpace, 1);
                }

                //画图片的前景噪音点  
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, System.Drawing.Color.FromArgb(random.Next()));
                }

                //画图片的边框线  
                g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
        #endregion
    }
}