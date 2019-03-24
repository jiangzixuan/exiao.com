using exiao.dll;
using exiao.model.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exiao.bll
{
    public class B_Agent
    {
        public static T_Agent AddAgent(string name, string shortName, string phone, string address, int adminId)
        {
            T_Agent a = new T_Agent() { Name = name, ShortName = shortName, Phone = phone, Address = address, AdminId = adminId, CreateDate=DateTime.Now };
            int id = D_Agent.AddAgent(a);
            if (id == 0) return null;
            a.Id = id;
            return a;
        }
    }
}
