using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NullLogBuilder.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/18 11:56:05         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/18 11:56:05          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log.Fluent
{
    public sealed class NDNullLogBuilder : INDLogBuilder 
    {
       public static readonly INDLogBuilder Instance = new NDNullLogBuilder();
        public LogData LogData
        {
            get { return null; }
        }

        public INDLogBuilder Message(Func<string> messageFormatter)
        {
            return this;
        }

        public INDLogBuilder Message(string message)
        {
            return this;
        }

        public INDLogBuilder Message(string format, object arg0)
        {
            return this;
        }

        public INDLogBuilder Message(string format, object arg0, object arg1)
        {
            return this;
        }

        public INDLogBuilder Message(string format, object arg0, object arg1, object arg2)
        {
            return this;
        }

        public INDLogBuilder Message(string format, object arg0, object arg1, object arg2, object arg3)
        {
            return this;
        }

        public INDLogBuilder Message(string format, params object[] args)
        {
            return this;
        }

        public INDLogBuilder Message(IFormatProvider provider, string format, params object[] args)
        {
            return this;
        }

        public INDLogBuilder Property(string name, object value)
        {
            return this;
        }

        public INDLogBuilder Exception(Exception exception)
        {
            return this;
        }

        public void Write(string callerMemberName = null, string callerFilePath = null, int callerLineNumber = 0)
        {
           
        }

        public void WriteIf(Func<bool> condition, string callerMemberName = null, string callerFilePath = null, int callerLineNumber = 0)
        {
           
        }

        public void WriteIf(bool condition, string callerMemberName = null, string callerFilePath = null, int callerLineNumber = 0)
        {
           
        }
    }
}
