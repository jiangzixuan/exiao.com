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
    }
}
