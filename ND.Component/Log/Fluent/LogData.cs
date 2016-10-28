using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LogData.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/18 11:45:36         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/18 11:45:36          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log.Fluent
{
   public sealed class LogData
    {
       private IDictionary<string, object> _properties;

       /// <summary>
       /// 日志级别
       /// </summary>
       public NDLogLevel LogLevel { get; set; }

    

       /// <summary>
       /// 消息格式化
       /// </summary>
       public Func<string> MessageFormatter { get; set; }

       /// <summary>
       /// 消息
       /// </summary>
       public string Message { get; set; }

       /// <summary>
       /// 参数
       /// </summary>
       public object[] Parameters { get; set; }

       /// <summary>
       /// 格式化提供器
       /// </summary>
       public IFormatProvider FormatProvider { get; set; }

       /// <summary>
       /// Exception
       /// </summary>
       public Exception Exception { get; set; }

       /// <summary>
       /// Gets or sets the name of the member.
       /// </summary>
       public string MemberName { get; set; }

       /// <summary>
       /// 文件路径
       /// </summary>
       public string FilePath { get; set; }

       /// <summary>
       /// 行号
       /// </summary>
       public int LineNumber { get; set; }

       public IDictionary<string, object> Properties
       {
           get { return _properties ?? (_properties = new Dictionary<String, Object>()); }
           set { _properties = value; }
       }

       public string GetMessage()
       {
           if (MessageFormatter != null)
               return MessageFormatter();

           if (Parameters != null && Parameters.Length > 0)
               return String.Format(FormatProvider, Message, Parameters);

           return Message;
       }

       public override string ToString()
       {
           return ToString(false, false);
       }

       public string ToString(bool includeFileInfo, bool includeException)
       {
           if (!includeFileInfo && !includeException)
               return GetMessage();

           var message = new StringBuilder();

           if (includeFileInfo && !String.IsNullOrEmpty(FilePath) && !String.IsNullOrEmpty(MemberName))
               message
                   .Append("[")
                   .Append(Path.GetFileName(FilePath))
                   .Append(" ")
                   .Append(MemberName)
                   .Append("()")
                   .Append(" Ln: ")
                   .Append(LineNumber)
                   .Append("] ");

           message.Append(GetMessage());

           if (includeException && Exception != null)
               message.Append(" ").Append(Exception);

           return message.ToString();
       }

    }
}
