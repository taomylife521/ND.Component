using ND.Component.Components;
using ND.Component.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NdConfigurationExtention.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-06-26 10:33:21         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-06-26 10:33:21          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log.Log4Net
{
    public static class NdConfigurationExtention
    {
        public static NdConfiguration UseLog4Net(this NdConfiguration configuration)
        {
            return UseLog4Net(configuration, "log4net.config");
            
        }
        public static NdConfiguration UseLog4Net(this NdConfiguration configuration, string configFile)
        {
            configuration.SetDefault<INDLoggerFactory, Log4NetLoggerFactory>(new Log4NetLoggerFactory(configFile));
            return configuration;
        }
    }
}
