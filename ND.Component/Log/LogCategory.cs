using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LogCategory.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/19 17:13:52         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/19 17:13:52          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log
{
    /// <summary>
    /// 目前支持的日志记录枚举类
    /// </summary>
    public enum LogCategory
    {
        /// <summary>
        /// Nlog
        /// </summary>
        NLog=0,

        /// <summary>
        /// Log4Net
        /// </summary>
        Log4Net=1,

        /// <summary>
        /// 数据库日志
        /// </summary>
        DBLog=2,

        /// <summary>
        /// 自定义日志
        /// </summary>
        Custom=3
    }
}
