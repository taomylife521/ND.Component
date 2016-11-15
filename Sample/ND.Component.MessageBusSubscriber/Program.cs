
using ND.Component.MessageBus;
using ND.Component.RabbitMQ.MessageBus;
using ND.Component.Redis.MessageBus;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.Component.MessageBusSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "订阅者";

            #region Redis
            //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
            //redis.PreserveAsyncOrder = false;//为确保安全，消息的传递默认是有序的。为获得更好的性能强烈建议你使用并行操作
            //ISubscriber sub = redis.GetSubscriber();
            ////sub.Subscribe("messages", (channel, message) =>
            ////{
            ////    Console.WriteLine((string)message);
            ////});
            //IMessageBus messageBus = new RedisMessageBus(sub, "messages");
            //Console.WriteLine("订阅Channel:messages");
            //messageBus.Subscribe<string>((message) =>
            //{

            //    Console.WriteLine("收到广播消息:" + (string)message);

            //}); 
            #endregion

            #region RabbitMQ
            IMessageBus messageBus = new RabbitMQMessageBus(hostNmae: "localhost", userName: "guest", password: "guest", queueName: "NDQueue", routingKey: "NDQueueRoutingKey",
                 exhangeName:"NDExchange", durable: false,persistent: false,exclusive: false,autoDelete: false, queueArguments: null);
            Console.WriteLine("Subscriber....");
            messageBus.Subscribe<string>(msg => { Console.WriteLine(msg); });
            Console.ReadLine();
            #endregion
            Console.ReadKey();
        }
    }
}
