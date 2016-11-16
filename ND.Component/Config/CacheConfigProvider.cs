using ND.Component.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CacheComponent.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/8 13:48:06         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/8 13:48:06          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Config
{
    /// <summary>
    /// 缓存节点类
    /// </summary>
    public class CacheConfigProvider : ConfigProviderBase
    {
        private bool isLogging = false;
        public CacheConfigProvider()
        {
            CacheItem = new List<CacheItem>();
        }
       

        /// <summary>
        /// 类型名称
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 缓存数据库名称
        /// </summary>
        public string CacheDBName { get; set; }

        /// <summary>
        /// 缓存数据表名称
        /// </summary>
        public string CacheTableName { get; set; }

        /// <summary>
        /// 是否记录日志
        /// </summary>
        public bool IsLogging { get { return isLogging; } set { isLogging = value; } }

        /// <summary>
        /// 缓存对象
        /// </summary>
        public ICache Cache { get; set; }

        /// <summary>
        /// 缓存服务器列表
        /// </summary>
        public List<CacheItem> CacheItem { get; set; }
    }
}
