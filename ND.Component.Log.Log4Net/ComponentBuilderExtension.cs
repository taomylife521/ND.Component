using ND.Component.ComponentBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：ComponentBuilderExtension.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/9 17:01:16         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/9 17:01:16          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log.Log4Net
{
   public static class ComponentBuilderExtension
    {
       public static IComponentBuilder UseLog4Net(this IComponentBuilder build)
       {
           NDLoggerFactoryManger.AddFactory(LogCategory.Log4Net, new Log4NetLoggerFactory());
           return build;
       }
    }
}
