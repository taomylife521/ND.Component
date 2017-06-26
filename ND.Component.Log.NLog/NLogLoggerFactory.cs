using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

//**********************************************************************
//
// 文件名称(File Name)：NLogLoggerProvider.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/19 11:02:57         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/19 11:02:57          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log.NLog
{
    public class NLogLoggerFactory : AbsNDLoggerFactory
    {
        public NLogLoggerFactory()
        {
            

        }
        public NLogLoggerFactory(string configFile)
        {
            var file = new FileInfo(configFile);
            if (!file.Exists)
            {
                file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile));
            }

            if (file.Exists)
            {
                //XmlLoggingConfiguration. 
                //XmlConfigurator.ConfigureAndWatch(file);
                XmlLoggingConfiguration.SetCandidateConfigFilePaths(new List<string>(){configFile});
            }
            else
            {
                SimpleConfigurator.ConfigureForTargetLogging(new ConsoleTarget() { Layout = new SimpleLayout() });
            }
            
        }
        protected override INDLogger CreateLogger(string name)
        {
           return new NLogLogger(name);
        }

        protected override INDLogger CreateLogger(Type type)
        {
            return new NLogLogger(type);
        }
    }
}
