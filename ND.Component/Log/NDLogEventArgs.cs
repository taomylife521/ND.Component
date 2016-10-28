using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NDLogEventArgs.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/20 10:08:21         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/20 10:08:21          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log
{
    public class NDLogEventArgs:EventArgs
    {
        public NDLogLevel LogLevel{get;set;}
        public object Message{get;set;}
        public Exception Exception{get;set;}
        
        public IFormatProvider Provider{get;set;}
         public object[] Args{get;set;}
    }
}
