using ND.Component.Log.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NullLogger.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/18 11:01:04         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/18 11:01:04          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log
{
    public class NullLogger : INDLogger 
    {
        public static readonly INDLogger Instance = new NullLogger();
        
        public bool IsEnabled(NDLogLevel logLevel)
        {
            return false;
        }

        public IDisposable BeginScope<TState, TScope>(Func<TState, TScope> scopeFactory, TState state)
        {
            return new EmptyDisposable();
        }

        public void Log<T>(NDLogLevel logLevel, T message, Exception exception, IFormatProvider provider, params object[] args) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
