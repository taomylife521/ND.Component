using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LogComponent.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/8 13:50:36         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/8 13:50:36          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Config
{
    public class LogConfigProvider
    {
        public string Type { get; set; }

        public bool IsEnabled { get; set; }
    }
}
