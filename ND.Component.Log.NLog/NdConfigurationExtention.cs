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
namespace ND.Component.Log.NLog
{
    public static class NdConfigurationExtention
    {
        public static NdConfiguration UseNLog(this NdConfiguration configuration)
        {
            configuration.SetDefault<INDLoggerFactory, NLogLoggerFactory>(new NLogLoggerFactory());
            return configuration;
        }
        public static NdConfiguration UseNLog(this NdConfiguration configuration, string configFile)
        {
            configuration.SetDefault<INDLoggerFactory, NLogLoggerFactory>(new NLogLoggerFactory(configFile));
            return configuration;
        }
    }
}
