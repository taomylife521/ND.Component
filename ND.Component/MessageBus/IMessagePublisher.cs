//**********************************************************************
//
// 文件名称(File Name)：        
// 功能描述(Description)：     
// 作者(Author)：               
// 日期(Create Date)： 2016/10/18 10:10:20         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期: 2016/10/18 10:10:20           
//             修改理由：         
//
//       R2:
//             修改作者:          
//             修改日期:  2016/10/18 10:10:20         
//             修改理由：         
//
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ND.Component.MessageBus
{
   public  interface IMessagePublisher
    {
        Task PublishAsync(Type messageType, object message, CancellationToken cancellationToken = default(CancellationToken));//TimeSpan? delay = null, 
    }
   public static class MessagePublisherExtensions
   {
       public static Task PublishAsync<T>(this IMessagePublisher publisher, T message) where T : class//, TimeSpan? delay = null
       {
           return publisher.PublishAsync(typeof(T), message);//delay
       }
   }
}
