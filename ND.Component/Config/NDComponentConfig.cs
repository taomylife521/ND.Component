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
        public CacheConfigProvider CacheProvider { get; set; }

        public MessageBusConfigProvider MessageBusProvider { get; set; }

        public LogConfigProvider LogProvider { get; set; }

        public QueueConfigProvider QueueProvider { get; set; }
       
    }
}
