using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LogBuilder.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/18 11:51:21         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/18 11:51:21          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log.Fluent
{
    public class NDLogBuilder : INDLogBuilder
    {
        private readonly LogData _data;
        private readonly INDLogger _logger;
        private static readonly Func<object, Exception, string> _messageFormatter = (state, error) => state.ToString();
        public NDLogBuilder(NDLogLevel logLevel, INDLogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException(logger.ToString());

            _logger = logger;
            _data = new LogData
            {
                LogLevel = logLevel,
                FormatProvider = CultureInfo.InvariantCulture
            };
        }
        public LogData LogData
        {
            get { return _data; }
        }
     
        public INDLogBuilder Message(Func<string> messageFormatter)
        {
            _data.MessageFormatter = messageFormatter;
            return this;
        }

        public INDLogBuilder Message(string message)
        {
            _data.Message = message;
            return this;
        }

        public INDLogBuilder Message(string format, object arg0)
        {
            _data.Message = format;
            _data.Parameters = new[] { arg0 };
            return this;
        }

        public INDLogBuilder Message(string format, object arg0, object arg1)
        {
            _data.Message = format;
            _data.Parameters = new[] { arg0, arg1 };
            return this;
        }

        public INDLogBuilder Message(string format, object arg0, object arg1, object arg2)
        {
            _data.Message = format;
            _data.Parameters = new[] { arg0, arg1, arg2 };
            return this;
        }

        public INDLogBuilder Message(string format, object arg0, object arg1, object arg2, object arg3)
        {
            _data.Message = format;
            _data.Parameters = new[] { arg0, arg1, arg2, arg3 };
            return this;
        }

        public INDLogBuilder Message(string format, params object[] args)
        {
            _data.Message = format;
            _data.Parameters = args;
            return this;
        }

        public INDLogBuilder Message(IFormatProvider provider, string format, params object[] args)
        {
            _data.FormatProvider = provider;
            _data.Message = format;
            _data.Parameters = args;
            return this;
        }

        public INDLogBuilder Property(string name, object value)
        {
            if (name == null)
                throw new ArgumentNullException(name.ToString());

            if (_data.Properties == null)
                _data.Properties = new Dictionary<string, object>();

            _data.Properties[name] = value;
            return this;
        }

        public INDLogBuilder Exception(Exception exception)
        {
            _data.Exception = exception;
            return this;
        }

        public void Write(string callerMemberName = null, string callerFilePath = null, int callerLineNumber = 0)
        {
            if (callerMemberName != null)
                _data.MemberName = callerMemberName;
            if (callerFilePath != null)
                _data.FilePath = callerFilePath;
            if (callerLineNumber != 0)
                _data.LineNumber = callerLineNumber;

            _logger.Log(LogData.LogLevel,  LogData, LogData.Exception, _messageFormatter);
        }

        public void WriteIf(Func<bool> condition, string callerMemberName = null, string callerFilePath = null, int callerLineNumber = 0)
        {
            Write(callerMemberName, callerFilePath, callerLineNumber);
        }

        public void WriteIf(bool condition, string callerMemberName = null, string callerFilePath = null, int callerLineNumber = 0)
        {
            Write(callerMemberName, callerFilePath, callerLineNumber);
        }
    }
}
