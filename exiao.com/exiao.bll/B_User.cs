using exiao.dll;
using exiao.model.entity;
using exiao.sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exiao.bll
{
    public class B_User
    {
        public static int Regedit(string userName, string password, string trueName, string Phone)
        {
            T_User u = new T_User() { UserName = userName, Password = Util.MD5(password), TrueName = trueName, Phone = Phone, CreateDate = DateTime.Now };
            int id = D_User.Regedit(u);
            return id;
        }

        public static bool IsUserNameExists(string userName)
        {
            T_User u = D_User.GetUser(userName);
            if (u == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static T_User Login(string userName, string password)
        {
            T_User u = D_User.GetUser(userName);
            if (u != null && u.Password == Util.MD5(password))
            {
                return u;
            }
            return null;
        }

        public static void LoginCookie(T_User u, bool isAutoLogin)
        {
            DateTime dt = DateTime.MinValue;
            if (isAutoLogin)
            {
                dt = DateTime.Now.AddDays(30);
            }
            UserCookieHelper.UserCookieModel m = new UserCookieHelper.UserCookieModel() { _id = u.Id, _uname = u.UserName, _ip = ClientUtil.Ip, _timestamp = Util.GetTimeStamp() };
            string uidentity = UserCookieHelper.EncryptUserCookie(m, Util.GetAppSetting("DesKey"));

            Util.SetCookie("exiao.user", "useridentity", uidentity, dt);
        }
    }
}
