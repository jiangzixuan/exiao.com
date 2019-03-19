using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exiao.sdk
{
    /// <summary>
    /// 定义系统中一些常量或者共用、不变的枚举/集合
    /// </summary>
    public class Const
    {
        /// <summary>
        /// 缓存Id中的项目级
        /// </summary>
        public enum CacheProject
        {
            EXiao
        }

        /// <summary>
        /// 缓存Id中的分类级
        /// </summary>
        public enum CacheCatalog
        {
            CheckCode=15     //验证码


        }

        /// <summary>
        /// 图片后缀名过滤集
        /// </summary>
        public static string[] ImgPattern = new string[] { "jpg", "jpeg", "png", "bmp" };

        /// <summary>
        /// 用户名过滤集
        /// </summary>
        public static string[] UserNameFilter = new string[] { "system", "admin", "sysadmin", "administrator" };

        #region  数据库连接字符串名称
        public enum DBName
        {
            EXiao
        }

        public static Dictionary<DBName, string> DBConnStrNameDic = new Dictionary<DBName, string>()
        {
            { DBName.EXiao, "Exiao" }
        };

        #endregion

    }
}
