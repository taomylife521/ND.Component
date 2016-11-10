using ND.Component.Caching;
using ND.Component.ComponentBuilder;
using ND.Component.Config;
using ND.Component.Log;
using ND.Component.Log.Log4Net;
using ND.Component.Log.NLog;
using ND.Component.MongoDB;
using ND.Component.Log.NLogComponent;
using ND.Component.MessageBus;
using ND.Component.Redis.MessageBus;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;



namespace ND.Component.Test
{
    class Program
    {
       
        static void Main(string[] args)
        {
          
          
            #region ND.Component.Log
            //NDLoggerFactoryManger.AddFactory(LogCategory.NLog, new NLogLoggerFactory());
           // NDLoggerFactoryManger.AddFactory(LogCategory.Custom, new CustomFileLoggerFactory());
           // NDLoggerFactoryManger.AddFactory(LogCategory.Log4Net, new Log4NetLoggerFactory());
           // NDLoggerFactoryManger.Instance.Error("i am error2", 1);
           
            #endregion

            #region MyRegion
            IComponentBuilder build = new DefaultComponentBuilder();
            build.Build()
                .UseLog4Net()
                .UseNLog()
                .UseMongoDBCache();

            Stopwatch wt = new Stopwatch();
            wt.Start();
            int sum = 0;
            int writeSucCount = 0;
            int writeFailedCount = 0;
            int rSucCount = 0;
            int rFailedCount = 0;
            int dSucCount = 0;//删除成功数量
            int dFailedCount = 0;//删除失败数量
            for (int i = 0; i < 10000; i++)//1万
            {

                bool r = CacheManger.Instance.SetValue(i.ToString(), "b" + i.ToString(), Cachelimit.ByExpireDate, DateTime.Now.AddMinutes(1));
                if (r)
                {
                    writeSucCount++;
                }
                else
                {
                    writeFailedCount++;
                }
                string bb = "";
                CacheManger.Instance.TryGetValue(i.ToString(), out bb);
                if (string.IsNullOrEmpty(bb))
                {
                    rFailedCount++;
                }
                else
                {
                    rSucCount++;
                }
                bool r2 = CacheManger.Instance.DeleteValue(i.ToString());
                if(r2)
                {
                    dSucCount++;
                }else
                {
                    dFailedCount++;
                }
                Console.WriteLine("set:{key:"+i.ToString()+",value:"+"b"+i.ToString()+"},get:{key:"+i.ToString()+",value:"+bb+"}"+"delete:"+r2);
                sum++;
               // Console.WriteLine("key:" + i.ToString() + ",value:" + bb);
            }
            wt.Stop();
            Console.WriteLine("耗时:" + wt.ElapsedMilliseconds / 1000);
            Console.WriteLine("写入成功率:" + writeSucCount / sum + ",写入失败率：" + writeFailedCount / sum);
            Console.WriteLine("读取成功率:" + rSucCount / sum + ",读取失败率：" + rFailedCount / sum);
            Console.WriteLine("删除成功率:" + dSucCount / sum + ",删除失败率：" + dFailedCount / sum);

            Console.WriteLine("All Keys:");
            CacheManger.Instance.GetAllKeys(CacheExpire.All).ForEach(x =>
            {
                Console.WriteLine(x);
            });
            #endregion

           


            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }

   
    
}
