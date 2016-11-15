
using ND.Component.MessageBus;
using ND.Component.RabbitMQ.MessageBus;
using ND.Component.Redis.MessageBus;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.Component.MessageBusPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "发布者";

            #region Redis
            //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
            //redis.PreserveAsyncOrder = false;//为确保安全，消息的传递默认是有序的。为获得更好的性能强烈建议你使用并行操作
            //ISubscriber sub = redis.GetSubscriber();
            //IMessageBus messageBus = new RedisMessageBus(sub, "messages");
            //Console.WriteLine("发布Channle:messages");
            //while (true)
            //{
            //    Console.WriteLine("请输入要发布的消息");
            //    string r = Console.ReadLine();
            //    messageBus.PublishAsync("发布广播消息:" + r);
            //    Console.WriteLine("ok!");
            //} 
            #endregion

            #region RabbitMQ
            IMessageBus messageBus = new RabbitMQMessageBus(hostNmae: "localhost", userName: "guest", password: "guest", queueName: "NDQueue", routingKey: "NDQueueRoutingKey",
                 exhangeName: "NDExchange", durable: false, persistent: false, exclusive: false, autoDelete: false, queueArguments: null);
            string input;
            Console.WriteLine("Publisher...");
            Console.WriteLine("Enter the messages to send (press CTRL+Z) to exit :");
            int index = 1;
            while (index < 11)
            {
                //input = Console.ReadLine();
               
                    Console.WriteLine(index);
                
                messageBus.PublishAsync(DateTime.Now+":"+index);
                index++;
            }
            Console.WriteLine("ok!");
            //messageBus.Dispose();
            #endregion
            Console.ReadKey();
        }
    }
}
