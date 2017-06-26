using ND.Component.Caching;
using ND.Component.ComponentBuilder;
using ND.Component.Config;
using ND.Component.Log;
using ND.Component.Log.Log4Net;
using ND.Component.Log.NLog;
using ND.Component.MongoDB;
using ND.Component.MessageBus;
using ND.Component.Redis.MessageBus;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

//**********************************************************************
//
// 文件名称(File Name)：TestLog.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/14 17:01:15         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/14 17:01:15          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Test
{
    public class TestLog
    {
       // INDLogger logger = NDLogManger.Instance.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void TestLog2()
         {
            // logger.Info("i am testlog");
         }
    }
}
