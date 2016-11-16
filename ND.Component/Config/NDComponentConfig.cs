using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NDComponentConfiguration.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/8 11:39:54         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/8 11:39:54          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Config
{
    /// <summary>
    /// Ndconfig组建构建
    /// </summary>
    public class NDComponentConfig : ConfigurationSection
    {
        public readonly static NDComponentConfig Instance=null;
        private bool isThrowException=false;
        private NDComponentConfig()
        {
           
        }
         static NDComponentConfig() 
        {
            if (Instance == null)
            {
                Instance = new NDComponentConfig();
            
            }
           
         }
        /// <summary>
        /// 配置组建异常是否抛出异常
        /// </summary>
        public bool IsThrowConfigException { get { return isThrowException; } set { isThrowException = value; } }

        /// <summary>
        /// 负载均衡算法配置节点
        /// </summary>
        public BalanceConfigProvider BalanceProvider { get; set; }

        /// <summary>
        /// 缓存日志提供节点
        /// </summary>
        public CacheConfigProvider CacheProvider { get; set; }


        /// <summary>
        /// 日志配置节点
        /// </summary>
        public LogConfigProvider LogProvider { get; set; }


        /// <summary>
        /// 消息总线配置节点
        /// </summary>
        public MessageBusConfigProvider MessageBusProvider { get; set; }

        /// <summary>
        /// 队列配置节点
        /// </summary>
        public QueueConfigProvider QueueProvider { get; set; }
       
    }
}
