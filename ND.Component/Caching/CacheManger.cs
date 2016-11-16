using ND.Component.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CacheManger.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/8 18:16:18         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/8 18:16:18          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Caching
{
    public class CacheManger
    {
        private static CacheManger _instance = null;
        private ICache _cache = NDComponentConfig.Instance.CacheProvider.Cache;
        private static readonly object _loadLock = new object();

        #region property
        public  ICache cache
        {
            get
            {
                return _cache;
            }
           
        }
        public static CacheManger Instance
        {
            get { return _instance; }
            private set { _instance = value; }
        }
        #endregion


      

        private CacheManger()
        {

        }
        static CacheManger()
        {
            if(_instance == null)
            {
                _instance = new CacheManger();
            }
           
        }
     
        public void RefreshCacheConfig(ICache cache)
        {
            _cache = cache;
        }

        public  object GetValue(string key)
        {
            return _cache.GetValue(key);
        }

        public  bool TryGetValue<T>(string key, out T value)
        {
            return _cache.TryGetValue(key, out value);
        }

        public  bool DeleteValue(string key)
        {
            return _cache.DeleteValue(key);
        }
        public  bool SetValue<T>(string key, T value, Cachelimit cacheLimit, DateTime? expireDate = null) where T:class
        {
            return _cache.SetValue<T>(key, value, cacheLimit, expireDate);
        }

        public List<string> GetAllKeys(CacheExpire cacheExpire = CacheExpire.All, DateType dType = DateType.CreateTime, DateTime? startDate = null, DateTime? endDate = null)
        {
           
               DateTime sDate =startDate == null ? DateTime.MinValue:Convert.ToDateTime(startDate);
               DateTime eDate = endDate == null ? DateTime.MaxValue : Convert.ToDateTime(endDate);
               return _cache.GetAllKeys(cacheExpire, dType, sDate, eDate);
        }
    }
}
