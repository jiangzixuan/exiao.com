using exiao.sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static exiao.sdk.Const;

namespace exiao.bll
{
    /// <summary>
    /// 存放验证码
    /// </summary>
    public class B_CheckCodeRedis
    {
        //缓存有效期(5分钟）
        private static TimeSpan ts = new TimeSpan(0, 5, 0);

        public static string GetCheckCode(string token)
        {
            string result = "";
            string key = RedisHelper.GetRedisKey(CacheCatalog.CheckCode, token);
            using (var client = RedisHelper.Instance.GetRedisClient(CacheCatalog.CheckCode.ToString()))
            {
                if (client != null)
                {
                    result = client.Get<string>(key);
                }
            }

            return result;
        }

        public static void SetCheckCode(string token, string checkCode)
        {
            string key = RedisHelper.GetRedisKey(CacheCatalog.CheckCode, token);
            using (var client = RedisHelper.Instance.GetRedisClient(CacheCatalog.CheckCode.ToString()))
            {
                if (client != null)
                {
                    client.Set<string>(key, checkCode, ts);
                }
            }
        }
    }
}
