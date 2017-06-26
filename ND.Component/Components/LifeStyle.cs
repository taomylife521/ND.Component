using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LifeStyle.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-06-20 17:08:32         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-06-20 17:08:32          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Components
{
    public enum LifeStyle
    {
        /// <summary>
        /// 瞬态对象
        /// </summary>
        Transient,

        /// <summary>
        /// 单例对象
        /// </summary>
        Singleton

    }
}
