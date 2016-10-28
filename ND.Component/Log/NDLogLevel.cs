using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LogLevel.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/18 10:37:40         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/18 10:37:40          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log
{
    public enum NDLogLevel
    {
      
        Trace = 0,

       
        Debug = 1,

      
        Information = 2,

      
        Warning = 3,

     
        Error = 4,

      
        Critical = 5,

      
        None = 6,
        
    }
}
