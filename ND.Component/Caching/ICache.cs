//**********************************************************************
//
// 文件名称(File Name)：        
// 功能描述(Description)：     
// 作者(Author)：               
// 日期(Create Date)： 2016/11/8 15:47:39         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期: 2016/11/8 15:47:39           
//             修改理由：         
//
//       R2:
//             修改作者:          
//             修改日期:  2016/11/8 15:47:39         
//             修改理由：         
//
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.Component.Caching
{
   public interface ICache
    {
        /// <summary>
        /// 不传过期时间默认是设置永久缓存
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        bool SetValue(string key, object value);

        /// <summary>
        /// 不传过期时间默认是设置永久缓存
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        bool SetValue<T>(string key, T value) where T : class;

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="value">缓存值</param>
        /// <param name="cacheLimit">缓存期限限制0-永久缓存 1-当天缓存</param>
        /// <returns></returns>
        bool SetValue<T>(string key, T value, Cachelimit cacheLimit = Cachelimit.CurrentDay, DateTime? expireDate = null) where T : class;

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="timeSpan">The time span.</param>
        bool SetValue(string key, object value, DateTime expireDate);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="timeSpan">The time span.</param>
        bool SetValue<T>(string key, T value, DateTime expireDate) where T : class;

        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Object.</returns>
        object GetValue(string key);

        /// <summary>
        /// 是否包含该缓存键
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if the specified key contains key; otherwise, <c>false</c>.</returns>
        bool ContainsKey(string key);

        /// <summary>
        /// 尝试获取该键的缓存值
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool TryGetValue(string key, out object value);

        /// <summary>
        /// 尝试获取该键的缓存值
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool TryGetValue<T>(string key, out T value);

        List<string> GetAllKeys(CacheExpire cacheExpire, DateType dateType, DateTime startDate, DateTime endDate);



        /// <summary>
        /// 获取缓存列表
        /// </summary>
        /// <param name="key">The key.</param>
        List<CacheKeyMapDescriptor> GetList(CacheExpire cacheExpire);

        /// <summary>
        /// 获取缓存列表
        /// </summary>
        /// <param name="key">The key.</param>
        List<CacheKeyMapDescriptor> GetList(CacheExpire cacheExpire, DateType dateType, DateTime startDate, DateTime endDate);

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">The key.</param>
        bool DeleteValue(string key);

        /// <summary>
        /// 批量清空所有的缓存
        /// </summary>
        /// <param name="key">The key.</param>
        bool BulkDeleteValue(CacheExpire cacheExpire);



        /// <summary>
        /// 批量清空所有的缓存
        /// </summary>
        /// <param name="key">The key.</param>
        bool BulkDeleteValue(CacheExpire cacheExpire, DateType dateType, DateTime startDate, DateTime endDate);
    }
}
