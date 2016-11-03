using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
namespace ND.Component.Log.NLogComponent
{
    public class NLogLoggerFactory : INDLoggerFactory
    {
        private NLogLogger logger = null;
        public INDLogger CreateLogger()
        {
            logger = new NLogLogger();
            return logger;
        }

        public INDLogger GetLogger()
        {
            return logger;
        }
    }
}
