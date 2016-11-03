
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NLogLogger.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/18 13:59:01         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/18 13:59:01          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log.NLogComponent
{
   public class NLogLogger:AbsNDLogger
    {

        private readonly Logger _logger = LogManager.GetLogger(LogCategory.NLog.ToString()); //LogManager.GetLogger(LogCategory.NLog.ToString());
        public override void Log<T>(NDLogLevel logLevel, T state, Exception exception,  IFormatProvider provider, params object[] args)
        {
            provider = null;
            var nLogLogLevel = ConvertLogLevel(logLevel);
            if (!IsEnabled(nLogLogLevel))
                return;

          
            switch(logLevel)
            {
                case NDLogLevel.Critical:
                    
                    _logger.Fatal(exception, provider, state.ToString(), args);
                    break;
                case NDLogLevel.Debug:
                    _logger.Debug(exception, provider, state.ToString(), args);
                    break;
                case NDLogLevel.Error:
                    _logger.Error(exception, provider, state.ToString(), args);
                    break;
                case NDLogLevel.Information:
                    _logger.Info(exception, provider, state.ToString(), args);
                    break;
                case NDLogLevel.None:
                    _logger.Info(exception, provider, state.ToString(), args);
                    break;
                case NDLogLevel.Trace:
                    _logger.Trace(exception, provider, state.ToString(), args);
                    break;
                case NDLogLevel.Warning:
                    _logger.Warn(exception, provider, state.ToString(), args);
                    break;
                default:
                    break;

            }
           
        }

        public override bool IsEnabled(NDLogLevel logLevel)
        {
            var convertLogLevel = ConvertLogLevel(logLevel);
            return IsEnabled(convertLogLevel);
        }
        private bool IsEnabled(global::NLog.LogLevel logLevel)
        {
           // return _logger.IsEnabled(logLevel);
            return true;
        }

        private static global::NLog.LogLevel ConvertLogLevel(NDLogLevel logLevel)
        {
            switch (logLevel)
            {
                case NDLogLevel.Debug:
                    return global::NLog.LogLevel.Debug;
                case NDLogLevel.Trace:
                    return global::NLog.LogLevel.Trace;
                case NDLogLevel.Information:
                    return global::NLog.LogLevel.Info;
                case NDLogLevel.Warning:
                    return global::NLog.LogLevel.Warn;
                case NDLogLevel.Error:
                    return global::NLog.LogLevel.Error;
                case NDLogLevel.Critical:
                    return global::NLog.LogLevel.Fatal;
                case NDLogLevel.None:
                    return global::NLog.LogLevel.Off;
                default:
                    return global::NLog.LogLevel.Debug;
            }
        }

       
       
    }
}
