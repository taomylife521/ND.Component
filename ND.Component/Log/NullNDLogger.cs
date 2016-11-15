using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NullNDLogger.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/15 10:52:13         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/15 10:52:13          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log
{
   public class NullNDLogger:INDLogger
    {
       public static readonly INDLogger Instance = new NullNDLogger();
        public void Log<T>(NDLogLevel logLevel, T message, Exception exception, IFormatProvider provider, params object[] args) where T : class
        {
           
        }

        public bool IsEnabled(NDLogLevel logLevel)
        {
            return false;
        }

        public INDLogger GetLogger(string name, Type type)
        {
            return Instance;
        }
    }
}
