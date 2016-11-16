using ND.Component.Log;
using ND.Component.Utility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：MessageBusBase.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/18 10:17:58         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/18 10:17:58          
//             修改理由：         
//**********************************************************************
namespace ND.Component.MessageBus
{
    public abstract class MessageBusBase :MaintenanceBase, IMessageBus 
    {
        public INDLogger logger = NDLogManger.Instance.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly ConcurrentDictionary<string, Subscriber> _subscribers = new ConcurrentDictionary<string, Subscriber>();
        public MessageBusBase()
        { 
        
        }
       // private readonly ConcurrentDictionary<Guid, DelayedMessage> _delayedMessages = new ConcurrentDictionary<Guid, DelayedMessage>();
        public virtual void Dispose()
        {
            this._subscribers.Clear();
           // this._delayedMessages.Clear();
           this.Dispose();

            
        }

       #region 发布消息
        public abstract Task PublishAsync(Type messageType, object message, CancellationToken cancellationToken = default(CancellationToken)); //TimeSpan? delay = null,
	#endregion

         #region 发送消息给订阅者
		protected async Task SendMessageToSubscribersAsync(Type messageType, object message) {
            if (message == null) {
                return;
            }

            var messageTypeSubscribers = _subscribers.Values.Where(s => s.Type==messageType).ToList();
            logger.Trace("Found {messageTypeSubscribers.Count} subscribers for message type {messageType.Name}.", messageTypeSubscribers.Count,messageTypeSubscribers.ToArray());
            foreach (var subscriber in messageTypeSubscribers) {
                if (subscriber.CancellationToken.IsCancellationRequested) {//如果取消发送则移除订阅者
                    Subscriber sub;
                    if (_subscribers.TryRemove(subscriber.Id, out sub))
                    {
                        logger.Trace("Removed cancelled subscriber: {subscriberId}", subscriber.Id);
                    }
                    else
                    {
                        logger.Trace("Unable to remove cancelled subscriber: {subscriberId}", subscriber.Id);
                    }
                    continue;
                }
                try {
                    await Task.FromResult(subscriber.Action(message, subscriber.CancellationToken));
                } catch (Exception ex) {
                    logger.Error(ex, "Error sending message to subscriber: {0}", ex.Message);
                }
            }
            logger.Trace("Done sending message to {messageTypeSubscribers.Count} subscribers for message type {messageType.Name}.", messageTypeSubscribers.Count, messageTypeSubscribers.ToArray());
           
        } 
	#endregion

         #region 订阅消息
		 public virtual void Subscribe<T>(Func<T, CancellationToken, Task> handler, CancellationToken cancellationToken = default(CancellationToken)) where T : class {
            var subscriber = new Subscriber {
                CancellationToken = cancellationToken,
                Type = typeof(T),
                Action = async (message, token) => {
                    if (!(message is T))
                        return;

                    await Task.FromResult(handler((T)message, cancellationToken));
                }
            };
            if (!_subscribers.TryAdd(subscriber.Id, subscriber))
            {
                logger.Error("Unable to add subscriber {subscriberId}", subscriber.Id);
            }
        } 
	#endregion

       #region 发送延迟消息
        // protected Task AddDelayedMessageAsync(Type messageType, object message, TimeSpan delay) {
        //    if (message == null)
        //        throw new ArgumentNullException(message.ToString());
            
        //    var sendTime = DateTime.UtcNow.Add(delay);
        //    _delayedMessages.TryAdd(Guid.NewGuid(), new DelayedMessage {
        //        Message = message,
        //        MessageType = messageType,
        //        SendTime = sendTime
        //    });
        //    return Task.FromResult(false);
        //   // ScheduleNextMaintenance(sendTime);
        //   // return Task.CompletedTask;
        //} 
	#endregion
        
    }

     internal class DelayedMessage {
            public DateTime SendTime { get; set; }
            public Type MessageType { get; set; }
            public object Message { get; set; }
        }
      public class Subscriber {
            public string Id { get{return Guid.NewGuid().ToString();} private set{} }
            public CancellationToken CancellationToken { get; set; }
            public Type Type { get; set; }
            public Func<object, CancellationToken, Task> Action { get; set; }
        }
}
