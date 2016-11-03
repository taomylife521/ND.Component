using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CustomFileLogger.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/20 11:17:48         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/20 11:17:48          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log.NLog
{
    public class CustomFileLogger:AbsNDLogger
    {
      public override void Log<T>(NDLogLevel logLevel, T message, Exception exception, IFormatProvider provider, params object[] args)
        {
            provider = null;
            File.WriteAllText("e://n.txt",logLevel.ToString()+":"+ message.ToString() + ",参数:" + string.Join(",", args));
           
        }

      public override bool IsEnabled(NDLogLevel logLevel)
      {
          return true;
      }
      
    }
}
