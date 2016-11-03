using ND.Component.Log.NLogComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CustomFileLoggerFactory.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/20 11:18:05         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/20 11:18:05          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log.NLog
{
    public class CustomFileLoggerFactory : INDLoggerFactory
    {
        private CustomFileLogger logger = null;
        public INDLogger CreateLogger()
        {
            logger = new CustomFileLogger();
            return logger;
        }

        public INDLogger GetLogger()
        {
            return logger;
        }
    }
}
