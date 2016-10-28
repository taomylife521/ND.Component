using ND.Component.Log;
using ND.Component.Log.Log4Net;
using ND.Component.Log.NLog;
using ND.Component.Log.NLogComponent;
using ND.Component.MessageBus;
using ND.Component.Redis.MessageBus;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ND.Component.Test
{
    class Program
    {
       
        static void Main(string[] args)
        {
          
          
            #region ND.Component.Log
            NDLoggerFactoryManger.AddFactory(LogCategory.NLog, new NLogLoggerFactory());
           // NDLoggerFactoryManger.AddFactory(LogCategory.Custom, new CustomFileLoggerFactory());
           // NDLoggerFactoryManger.AddFactory(LogCategory.Log4Net, new Log4NetLoggerFactory());
            NDLoggerFactoryManger.Instance.Error("i am error2", 1);
           
            #endregion

           


            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }

   
    
}
