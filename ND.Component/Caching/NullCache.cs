using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NullCache.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/16 14:41:16         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/16 14:41:16          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Caching
{
   public class NullCache:ICache
    {
        public bool SetValue(string key, object value)
        {
            return SetValue(key,value,Cachelimit.CurrentDay,null);
        }

        public bool SetValue<T>(string key, T value) where T : class
        {
            return SetValue(key, value, Cachelimit.CurrentDay, null);
        }

        public bool SetValue<T>(string key, T value, Cachelimit cacheLimit = Cachelimit.CurrentDay, DateTime? expireDate = null) where T : class
        {
            return false;
        }

        public bool SetValue(string key, object value, DateTime expireDate)
        {
            return SetValue(key, value, Cachelimit.CurrentDay, expireDate);
        }

        public bool SetValue<T>(string key, T value, DateTime expireDate) where T : class
        {
            return SetValue(key, value, Cachelimit.CurrentDay, expireDate);
        }

        public object GetValue(string key)
        {
            return null;
        }

        public bool ContainsKey(string key)
        {
            return false;
        }

        public bool TryGetValue(string key, out object value)
        {
            value = null;
            return false;
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            value = default(T);
            return false;
            
        }

        public List<string> GetAllKeys(CacheExpire cacheExpire, DateType dateType, DateTime startDate, DateTime endDate)
        {
            return new List<string>();
        }

        public List<CacheKeyMapDescriptor> GetList(CacheExpire cacheExpire)
        {
            return new List<CacheKeyMapDescriptor>();
        }

        public List<CacheKeyMapDescriptor> GetList(CacheExpire cacheExpire, DateType dateType, DateTime startDate, DateTime endDate)
        {
            return new List<CacheKeyMapDescriptor>();
        }

        public bool DeleteValue(string key)
        {
            return false;
        }

        public bool BulkDeleteValue(CacheExpire cacheExpire)
        {
            return false;
        }

        public bool BulkDeleteValue(CacheExpire cacheExpire, DateType dateType, DateTime startDate, DateTime endDate)
        {
            return false;
        }
    }
}
