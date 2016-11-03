//**********************************************************************
//
// 文件名称(File Name)：        
// 功能描述(Description)：     
// 作者(Author)：               
// 日期(Create Date)： 2016/10/18 10:37:01         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期: 2016/10/18 10:37:01           
//             修改理由：         
//
//       R2:
//             修改作者:          
//             修改日期:  2016/10/18 10:37:01         
//             修改理由：         
//
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.Component.Log
{
    /// <summary>
    /// 日志记录接口
    /// </summary>
    public interface INDLogger
    {
        void Log<T>(NDLogLevel logLevel,T message, Exception exception,IFormatProvider provider,params object[] args) where T:class;
        bool IsEnabled(NDLogLevel logLevel);
       
    }
    public interface ILogger<T> : INDLogger { }
}
