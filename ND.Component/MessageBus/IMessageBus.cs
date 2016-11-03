//**********************************************************************
//
// 文件名称(File Name)：        
// 功能描述(Description)：     
// 作者(Author)：               
// 日期(Create Date)： 2016/10/18 10:16:50         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期: 2016/10/18 10:16:50           
//             修改理由：         
//
//       R2:
//             修改作者:          
//             修改日期:  2016/10/18 10:16:50         
//             修改理由：         
//
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.Component.MessageBus
{
   public interface IMessageBus: IMessagePublisher, IMessageSubscriber, IDisposable 
    {
    }
}
