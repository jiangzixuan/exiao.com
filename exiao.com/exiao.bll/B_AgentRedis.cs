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
    public class B_AgentRedis
    {
        //缓存有效期(30天）
        private static TimeSpan ts = new TimeSpan(30, 0, 0, 0);

        /// <summary>
        /// 根据adminId查询Agent
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public static List<T_Agent> GetAgentByAdminId(int adminId)
        {
            List<T_Agent> u = null;
            string key = RedisHelper.GetRedisKey(Const.CacheCatalog.Agent, adminId.ToString());
            using (var client = RedisHelper.Instance.GetRedisClient(Const.CacheCatalog.Agent.ToString()))
            {
                if (client != null)
                {
                    u = client.Get<List<T_Agent>>(key);
                    if (u == null)
                    {
                        u = D_Agent.GetAgentByAdminId(adminId);
                        if (u != null)
                        {
                            client.Set<List<T_Agent>>(key, u, ts);
                        }
                    }
                }
            }

            return u;
        }
    }
}
