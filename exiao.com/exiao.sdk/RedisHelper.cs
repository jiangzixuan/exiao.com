﻿using ServiceStack;
using ServiceStack.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static exiao.sdk.Const;

namespace exiao.sdk
{
    /// <summary>
    /// redis访问帮助类
    /// </summary>
    public class RedisHelper
    {
        private static volatile RedisHelper _instance = null;

        private static object _lock = new object();

        private const string RedisConfigName = "RedisConfig.xml";
        /// <summary>
        /// 单实例的RedisClient字典
        /// </summary>
        private readonly ConcurrentDictionary<string, IRedisClient> RedisCatelogDictionary;

        /// <summary>
        /// 缓存池方式的RedisClient字典
        /// </summary>
        private readonly ConcurrentDictionary<string, PooledRedisClientManager>  PoolInstanceDictonary;

        private RedisHelper()
        {
            RedisCatelogDictionary = new ConcurrentDictionary<string, IRedisClient>();

            PoolInstanceDictonary = new ConcurrentDictionary<string, PooledRedisClientManager>();
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        public static RedisHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new RedisHelper();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 根据字库类别查询RedisConfig.xml获取各字库的信息
        /// </summary>
        /// <param name="catelog"></param>
        /// <returns></returns>
        private static RedisModel GetRedisModel(string catelog)
        {
            RedisModel r = null;
            string path = AppDomain.CurrentDomain.BaseDirectory + "/" + RedisConfigName;
            if (!File.Exists(path)) return null;
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            
            XmlNode xn = doc.SelectSingleNode("root");
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode xnr in xnl)
            {
                XmlElement xe = (XmlElement)xnr;
                if (xe.GetAttribute("catelog") == catelog)
                {
                    r = new RedisModel();
                    r.Ip = xe.GetAttribute("ip");
                    r.Port = int.Parse(xe.GetAttribute("port"));
                    r.Pwd = xe.GetAttribute("pwd");
                    r.Db = long.Parse(xe.GetAttribute("db"));
                    break;
                }
            }
            if (r == null)
            {
                LogHelper.Error("RedisConfig.xml文件中未找到catelog:" + catelog);
            }
            return r;
        }

        public IRedisClient GetRedisClient(string catelog)
        {
            #region 单例模式
            //GetRedisClientBySingle(catelog);
            #endregion

            #region 集群方式
            return GetRedisClientByPool(catelog);
            #endregion
        }

        /// <summary>
        /// 单实例方式
        /// </summary>
        /// <param name="catelog"></param>
        /// <returns></returns>
        private IRedisClient GetRedisClientBySingle(string catelog)
        {
            IRedisClient result;
            bool b = RedisCatelogDictionary.TryGetValue(catelog, out result);
            if (!b)
            {
                RedisModel rm = GetRedisModel(catelog);
                result = new RedisClient(rm.Ip, rm.Port, rm.Pwd, rm.Db);
                RedisCatelogDictionary.TryAdd(catelog, result);
            }
            return result;
        }

        /// <summary>
        /// 缓存池方式
        /// </summary>
        /// <param name="catelog"></param>
        /// <returns></returns>
        private IRedisClient GetRedisClientByPool(string catelog)
        {
            PooledRedisClientManager result;
            bool b = PoolInstanceDictonary.TryGetValue(catelog, out result);
            if (!b)
            {
                result = InitPool(catelog);
            }
            if (result == null) return null;
            return result.GetClient();
        }

        /// <summary>
        /// 初始化Pool
        /// </summary>
        /// <param name="catelog"></param>
        /// <returns></returns>
        private PooledRedisClientManager InitPool(string catelog)
        {   
            int _RedisPoolSize = 10000;
            int _RedisPoolTimeoutSeconds = 2;
            RedisModel _rm = GetRedisModel(catelog);
            if (_rm == null) return null;
            PooledRedisClientManager Instance = new PooledRedisClientManager(_RedisPoolSize,
                                       _RedisPoolTimeoutSeconds,
                                       new string[] { string.Format("{0}@{1}:{2}", _rm.Pwd, _rm.Ip, _rm.Port) })
            {
                ConnectTimeout = 10000 // 1500
            };
            PoolInstanceDictonary.TryAdd(catelog, Instance);
            return Instance;
    }

        /// <summary>
        /// CacheProject = EasyZy
        /// </summary>
        /// <param name="cc"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetRedisKey(CacheCatalog cc, string key)
        {
            return "C_" + (int)CacheProject.EXiao + "_" + (int)cc + "_" + key;
        }

        //public static T ConvertDicToEntitySingle<T>(Dictionary<string, string> dic) where T : new()
        //{
        //    if (dic == null)
        //    {
        //        return default(T);
        //    }
        //    return dic.ToJson().FromJson<T>();

        //}

        //public static List<T> ConvertDicToEntitySingle<T>(List<Dictionary<string, string>> dic) where T : new()
        //{
        //    if (dic == null)
        //    {
        //        return new List<T>();
        //    }
        //    List<T> result = new List<T>();
        //    foreach (var d in dic)
        //    {
        //        result.Add(ConvertDicToEntitySingle<T>(d));
        //    }
        //    return result;
        //}
    }

    public class RedisModel
    {
        public string Ip { get; set; }

        public int Port { get; set; }

        public string Pwd { get; set; }

        public long Db { get; set; }
    }
}
