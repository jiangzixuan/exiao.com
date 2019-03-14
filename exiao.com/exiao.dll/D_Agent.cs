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
        public List<T_Agent> SearchAgents(int adminId)
        {
            List<T_Agent> model = null;
            using (MySqlDataReader dr = MySqlHelper.ExecuteReader(Util.GetEXiaoConnectString(),
                "select Id, ZyId, ZyType, StudentId, AnswerJson, AnswerImg, Submited, CreateDate from T_Answer where ZyId = @ZyId and Submited = 1",
                "@ZyId".ToInt32InPara(adminId)))
            {
                if (dr != null && dr.HasRows)
                {
                    model = MySqlDBHelper.ConvertDataReaderToEntityList<T_Agent>(dr);
                }
            }
            return model;
        }
    }
}
