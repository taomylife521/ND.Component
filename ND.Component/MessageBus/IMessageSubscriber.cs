using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：IMessageSubscriber.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/18 10:11:38         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/18 10:11:38          
//             修改理由：         
//**********************************************************************
namespace ND.Component.MessageBus
{
    public interface IMessageSubscriber
    {
        void Subscribe<T>(Func<T, CancellationToken, Task> handler, CancellationToken cancellationToken = default(CancellationToken)) where T : class;
    }
    public static class MessageBusExtensions
    {
        public static void Subscribe<T>(this IMessageSubscriber subscriber, Func<T, Task> handler, CancellationToken cancellationToken = default (CancellationToken)) where T : class
        {
            subscriber.Subscribe<T>((msg, token) => handler(msg), cancellationToken);
        }

        public static void Subscribe<T>(this IMessageSubscriber subscriber, Action<T> handler, CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            subscriber.Subscribe<T>((msg, token) =>
            {
                handler(msg);
                return Task.FromResult(true);
            }, cancellationToken);
        }
    }
}
