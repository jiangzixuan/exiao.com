using exiao.dll;
using exiao.model.dto;
using exiao.model.entity;
using exiao.sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exiao.bll
{
    public class B_UserRedis
    {
        //缓存有效期(30天）
        private static TimeSpan ts = new TimeSpan(30, 0, 0, 0);


        /// <summary>
        /// 根据UserId查询User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Dto_User GetUser(int userId)
        {
            Dto_User u = null;
            string key = RedisHelper.GetRedisKey(Const.CacheCatalog.User, userId.ToString());
            using (var client = RedisHelper.Instance.GetRedisClient(Const.CacheCatalog.User.ToString()))
            {
                if (client != null)
                {
                    u = client.Get<Dto_User>(key);
                    if (u == null)
                    {
                        u = D_User.GetUser(userId);
                    }
                    if (u != null && u.Agents == null)
                    {
                        u.Agents = D_Agent.GetAgentByAdminId(u.Id);
                        client.Set(key, u, ts);
                    }
                }
            }

            return u;
        }
    }
}
