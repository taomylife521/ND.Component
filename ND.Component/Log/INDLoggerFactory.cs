//**********************************************************************
//
// 文件名称(File Name)：        
// 功能描述(Description)：     
// 作者(Author)：               
// 日期(Create Date)： 2016/10/18 10:40:30         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期: 2016/10/18 10:40:30           
//             修改理由：         
//
//       R2:
//             修改作者:          
//             修改日期:  2016/10/18 10:40:30         
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
    /// 日志记录工厂类
    /// </summary>
    public interface INDLoggerFactory
    {
        INDLogger GetLogger(Type type);
        INDLogger GetLogger(string name);
        
    }
}
