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
    public class D_Agent
    {
        public static List<T_Agent> GetAgentByAdminId(int adminId)
        {
            List<T_Agent> model = null;
            using (MySqlDataReader dr = MySqlHelper.ExecuteReader(Util.GetEXiaoConnectString(),
                "select Id, Name, ShortName, Address, Phone, AdminId, CreateDate from T_Agent where AdminId = @AdminId",
                "@AdminId".ToInt32InPara(adminId)))
            {
                if (dr != null && dr.HasRows)
                {
                    model = MySqlDBHelper.ConvertDataReaderToEntityList<T_Agent>(dr);
                }
            }
            return model;
        }

        public static int AddAgent(T_Agent a)
        {
            object o = MySqlHelper.ExecuteScalar(Util.GetEXiaoConnectString(),
                "insert into T_Agent(Id, Name, ShortName, Phone, Address, AdminId, CreateDate) values (null, @Name, @ShortName, @Phone, @Address, @AdminId,  @CreateDate); select last_insert_id();",
                "@Name".ToVarCharInPara(a.Name),
                "@ShortName".ToVarCharInPara(a.ShortName),
                "@Phone".ToVarCharInPara(a.Phone),
                "@Address".ToVarCharInPara(a.Address),
                "@AdminId".ToInt32InPara(a.AdminId),
                "@CreateDate".ToDateTimeInPara(a.CreateDate)
                );
            return o == null ? 0 : int.Parse(o.ToString());
        }
    }
}
