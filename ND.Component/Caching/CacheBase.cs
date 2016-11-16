using ND.Component.Config;
using ND.Component.LoadBalance;
using ND.Component.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CacheBase.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/8 15:56:21         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/8 15:56:21          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Caching
{
    public abstract class CacheBase:ICache
    {
        #region Event
        public static event EventHandler<string> onOperating;
        public List<Server> serverConfig = new List<Server>();
        public string CacheDBName = "CacheDB";
        public string CacheTableName = "CacheTable";
        public CacheBase()
        {
            if(NDComponentConfig.Instance.CacheProvider ==null || NDComponentConfig.Instance.CacheProvider.IsEnabled!=true || NDComponentConfig.Instance.CacheProvider.CacheItem == null || NDComponentConfig.Instance.CacheProvider.CacheItem.Where(x=>x.IsEnabled==true).ToList().Count <= 0)
            {
                throw new Exception("Invalid_CacheProvider_Config");
            }
            CacheDBName = NDComponentConfig.Instance.CacheProvider.CacheDBName;
            CacheTableName = NDComponentConfig.Instance.CacheProvider.CacheTableName;
            NDComponentConfig.Instance.CacheProvider.CacheItem.ForEach(x =>
            {
                if (x.IsEnabled)//如果启用则添加到服务器列表中
                {
                    serverConfig.Add(new Server(x.ConnStr, x.WeightValue, x.ConnStr));
                }
            });
        }

        public Server ChooseServer(string key)
        {
            return LoadBalanceManger.Instance.ChooseServer(serverConfig, key);
        }

        public string ChooseServerConnStr(string key)
        {
            return LoadBalanceManger.Instance.ChooseServer(serverConfig, key).ConnStr;
        }

        /// <summary>
        /// 操作时
        /// </summary>
        /// <param name="e"></param>
        public void OnOperating(string e)
        {
            try
            {
                if (NDComponentConfig.Instance.CacheProvider.IsLogging)//如果要记录日志的话
                {
                   // NDLoggerFactoryManger.Instance.Info(e);

                }
            }
            catch (Exception ex)
            { }
            if (onOperating != null)
            {
                onOperating(this, e);
               
            }
        }


        #endregion

        #region Set
        public virtual bool SetValue(string key, object value)
        {
            return SetValue(key, value, Cachelimit.CurrentDay, Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"));
        }


        public virtual bool SetValue<T>(string key, T value) where T : class
        {
            return SetValue(key, value, Cachelimit.CurrentDay, Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"));
        }

        public abstract bool SetValue<T>(string key, T value, Cachelimit cacheLimit, DateTime? expireDate) where T : class;

        bool ICache.SetValue(string key, object value, DateTime expireDate)
        {
            return SetValue(key, value, Cachelimit.ByExpireDate, expireDate);
        }


        bool ICache.SetValue<T>(string key, T value, DateTime expireDate)
        {
            return SetValue(key, value, Cachelimit.ByExpireDate, expireDate);
        }






        #endregion

        #region Get
        public abstract object GetValue(string key);

        public virtual bool ContainsKey(string key) {
            return GetValue(key) == null ? false : true;
        }
       

        public virtual bool TryGetValue(string key, out object value)
        {
            return TryGetValue(key, out value);
        }

        public abstract bool TryGetValue<T>(string key, out T value);
        

      
        #endregion

      
        public abstract bool DeleteValue(string key);

        public abstract List<string> GetAllKeys(CacheExpire cacheExpire, DateType dateType, DateTime startDate, DateTime endDate);






        public List<CacheKeyMapDescriptor> GetList(CacheExpire cacheExpire)
        {
            return GetList(cacheExpire, DateType.CreateTime, Convert.ToDateTime("1900-01-01"), DateTime.Now);
        }

        public abstract List<CacheKeyMapDescriptor> GetList(CacheExpire cacheExpire, DateType dateType, DateTime startDate, DateTime endDate);
       

        public bool BulkDeleteValue(CacheExpire cacheExpire)
        {
            return BulkDeleteValue(cacheExpire, DateType.CreateTime, Convert.ToDateTime("1900-01-01"), DateTime.Now);
        }

        public abstract bool BulkDeleteValue(CacheExpire cacheExpire, DateType dateType, DateTime startDate, DateTime endDate);
       
    }
}
