using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NullMessageBus.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/20 14:53:10         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/20 14:53:10          
//             修改理由：         
//**********************************************************************
namespace ND.Component.MessageBus
{
    public class NullMessageBus : IMessageBus
    {
        public static readonly NullMessageBus Instance = new NullMessageBus();
        public void Subscribe<T>(Func<T, System.Threading.CancellationToken, Task> handler, System.Threading.CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
           
        }

        public void Dispose()
        {
           
        }

        public Task PublishAsync(Type messageType, object message, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(false);
        }
    }
}
