using exiao.model.entity;
using exiao.sdk;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exiao.dll
{
    public class D_User
    {
        public static T_User GetUser(string userName)
        {
            T_User model = null;
            using (MySqlDataReader dr = MySqlHelper.ExecuteReader(Util.GetEXiaoConnectString(),
                "select Id, UserName, TrueName, Password, Phone, CreateDate from T_User where UserName = @UserName",
                "@UserName".ToVarCharInPara(userName)))
            {
                if (dr != null && dr.HasRows)
                {
                    model = MySqlDBHelper.ConvertDataReaderToEntitySingle<T_User>(dr);
                }
            }
            return model;
        }

        public static int Regedit(T_User u)
        {
            object o = MySqlHelper.ExecuteScalar(Util.GetEXiaoConnectString(),
                "insert into T_User(Id, UserName, Password, TrueName, Phone, CreateDate) values (null, @UserName, @Password, @TrueName, @Phone, @CreateDate); select last_insert_id();",
                "@UserName".ToVarCharInPara(u.UserName),
                "@Password".ToVarCharInPara(u.Password),
                "@TrueName".ToVarCharInPara(u.TrueName),
                "@Phone".ToVarCharInPara(u.Phone),
                "@CreateDate".ToDateTimeInPara(u.CreateDate)
                );
            return o == null ? 0 : int.Parse(o.ToString());
        }

        public static T_User GetUser(int userId)
        {
            T_User model = null;
            using (MySqlDataReader dr = MySqlHelper.ExecuteReader(Util.GetEXiaoConnectString(),
                "select Id, UserName, TrueName, Password, Phone, CreateDate from T_User where Id = @Id",
                "@Id".ToInt32InPara(userId)))
            {
                if (dr != null && dr.HasRows)
                {
                    model = MySqlDBHelper.ConvertDataReaderToEntitySingle<T_User>(dr);
                }
            }
            return model;
        }

    }
}
