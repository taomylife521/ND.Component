using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CacheKeyMapDescriptor.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/8 15:49:52         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/8 15:49:52          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Caching
{
    public class CacheKeyMapDescriptor
    {
        private Cachelimit _cachelimit = Cachelimit.ByExpireDate;

        // [BsonId]
        // public string CacheKeyMapId { get { return Guid.NewGuid().ToString("N"); } }
        /// <summary>
        /// 缓存key
        /// </summary>
        public string CacheKey { get; set; }

        /// <summary>
        /// 缓存id
        /// </summary>
       
        public string CacheServerName { get; set; }

        /// <summary>
        /// 缓存限制
        /// </summary>
       // public Cachelimit Cachelimit { get { return _cachelimit; } set { _cachelimit = value; } }

        /// <summary>
        /// 缓存时间
        /// </summary>
  
        public DateTime ExpireDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否有效 0-无效 1-有效
        /// </summary>
        public CacheStatus CacheSta { get; set; }
    }
}
