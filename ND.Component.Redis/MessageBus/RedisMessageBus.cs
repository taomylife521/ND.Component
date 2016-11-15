using ND.Component.MessageBus;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ND.Component.Redis.Extentions;
using ND.Component.Utility;
using ND.Component.Log;

//**********************************************************************
//
// 文件名称(File Name)：RedisMessageBus.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/20 14:59:21         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/20 14:59:21          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Redis.MessageBus
{
    public class RedisMessageBus : MessageBusBase
    {
        private readonly ISubscriber _subscriber;
        private readonly string _topic;
        //private readonly ISerializer _serializer;
        private static readonly object _lockObject = new object();
        private bool _isSubscribed;
        public RedisMessageBus(ISubscriber subscriber, string topic = null)
          
        {
            _subscriber = subscriber;
            _topic = topic ?? "messages";
          
        }

        private void EnsureTopicSubscription()
        {
            if (_isSubscribed)
                return;

            lock (_lockObject)
            {
                if (_isSubscribed)
                    return;
                
                _subscriber.Subscribe(_topic, OnMessage);
                _isSubscribed = true;
            }
        }

        private async void OnMessage(RedisChannel channel, RedisValue value)
        {


            var message = await JsonConvert.DeserializeObjectAsync<MessageBusData>((string)value).AnyContext();

            Type messageType;
            try
            {
                messageType = Type.GetType(message.Type);
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, "Error getting message body type: {0}", ex.Message);
                return;
            }

            object body = await JsonConvert.DeserializeObjectAsync(message.Data, messageType, new JsonSerializerSettings()).AnyContext(); 
            await SendMessageToSubscribersAsync(messageType, body).AnyContext(); 
        }

        public override async Task PublishAsync(Type messageType, object message, System.Threading.CancellationToken cancellationToken = default(CancellationToken))//TimeSpan? delay = null,
        {
            if (message == null)
                return;


           // if (delay.HasValue && delay.Value > TimeSpan.Zero)
            //{
                //_logger.Trace("Schedule delayed message: {messageType} ({delay}ms)", messageType.FullName, delay.Value.TotalMilliseconds);
                //await AddDelayedMessageAsync(messageType, message, delay.Value).AnyContext();
               // return;
           // }

          //  _logger.Trace("Message Publish: {messageType}", messageType.FullName);
            var data = await JsonConvert.SerializeObjectAsync(new MessageBusData
            {
                Type = messageType.AssemblyQualifiedName,
                Data = await JsonConvert.SerializeObjectAsync(message).AnyContext()
            }).AnyContext();
         
            await Run.WithRetriesAsync(
                () => _subscriber.PublishAsync(_topic, data, CommandFlags.FireAndForget), 
                cancellationToken: cancellationToken).AnyContext();
        }

        public override void Subscribe<T>(Func<T, CancellationToken, Task> handler, CancellationToken cancellationToken = default(CancellationToken))
        {
            EnsureTopicSubscription();
            base.Subscribe(handler, cancellationToken);
        }

        #region Dispose
        public override void Dispose()
        {
            base.Dispose();

            if (_isSubscribed)
            {
                lock (_lockObject)
                {
                    if (!_isSubscribed)
                        return;

                    _subscriber.UnsubscribeAll(CommandFlags.FireAndForget);
                    _isSubscribed = false;
                }
            }
        } 
        #endregion
    }
}
