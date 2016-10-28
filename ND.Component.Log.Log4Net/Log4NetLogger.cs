using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace ND.Component.Log.Log4Net
{
    public class Log4NetLogger : AbsNDLogger
    {
       
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(LogCategory.Log4Net.ToString());
          
        //private readonly Logger _logger = LogManager.GetLogger(LogCategory.NLog.ToString()); //LogManager.GetLogger(LogCategory.NLog.ToString());
        public override void Log<T>(NDLogLevel logLevel, T message, Exception exception, IFormatProvider provider, params object[] args)
        {
            provider = null;
            var log4netLevel = ConvertLogLevel(logLevel);
            if (!IsEnabled(log4netLevel))
                return;


            switch (logLevel)
            {
                case NDLogLevel.Critical:
                    _logger.Fatal(message,exception);
                    break;
                case NDLogLevel.Debug:
                    _logger.Debug(message,exception);
                    break;
                case NDLogLevel.Error:
                    _logger.Error(message, exception);
                    break;
                case NDLogLevel.Information:
                    _logger.Info(message, exception);
                    break;
                case NDLogLevel.None:
                    _logger.Info(message, exception);
                    break;
                case NDLogLevel.Trace:
                    _logger.Info(message, exception);
                    break;
                case NDLogLevel.Warning:
                    _logger.Warn(message, exception);
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
        private bool IsEnabled(global::log4net.Core.Level logLevel)
        {
            // return _logger.IsEnabled(logLevel);
            return true;
        }

        private static global::log4net.Core.Level ConvertLogLevel(NDLogLevel logLevel)
        {
            switch (logLevel)
            {
                case NDLogLevel.Debug:
                    return global::log4net.Core.Level.Debug;
                case NDLogLevel.Trace:
                    return global::log4net.Core.Level.Trace;
                case NDLogLevel.Information:
                    return global::log4net.Core.Level.Info;
                case NDLogLevel.Warning:
                    return global::log4net.Core.Level.Warn;
                case NDLogLevel.Error:
                    return global::log4net.Core.Level.Error;
                case NDLogLevel.Critical:
                    return global::log4net.Core.Level.Fatal;
                case NDLogLevel.None:
                    return global::log4net.Core.Level.Off;
                default:
                    return global::log4net.Core.Level.Debug;
            }
        }
    }
}
