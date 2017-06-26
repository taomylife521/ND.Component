using ND.Component.Caching;
using ND.Component.ComponentBuilder;
using ND.Component.Config;
using ND.Component.Log;
using ND.Component.Log.Log4Net;
using ND.Component.Log.NLog;
using ND.Component.MongoDB;
using ND.Component.MessageBus;
using ND.Component.Redis.MessageBus;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using ND.Component.Autofac;
using ND.Component.Configurations;
using ND.Component.Components;
using System.Runtime.InteropServices;


namespace ND.Component.Test
{
    class Program
    {
        public static string getMemory(object o) // 获取引用类型的内存地址方法
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.Pinned);
            IntPtr addr = h.AddrOfPinnedObject();
            return "0x" + addr.ToString("X");
        }
       
        static void Main(string[] args)
        {
          
            //Init
            //IComponentBuilder build = new DefaultComponentBuilder();
           // build.Build(new XmlConfigBuilder());
               
            #region 日志测试

            //INDLogger logger = NDLogManger.Instance.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            //try
            //{
              
            //    logger.Info("i am program", new Exception("错误了"));
            //    TestLog log = new TestLog();
            //    log.TestLog2();
            //    logger.Info("i am program2");
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex);
            //} 
            #endregion

            #region Cache 测试

            //Stopwatch wt = new Stopwatch();
            //wt.Start();
            //int sum = 0;
            //int writeSucCount = 0;
            //int writeFailedCount = 0;
            //int rSucCount = 0;
            //int rFailedCount = 0;
            //int dSucCount = 0;//删除成功数量
            //int dFailedCount = 0;//删除失败数量
            //for (int i = 0; i < 1; i++)//1万
            //{

            //    bool r = CacheManger.Instance.SetValue(i.ToString(), "b" + i.ToString(), Cachelimit.ByExpireDate, DateTime.Now.AddSeconds(10));
            //    if (r)
            //    {
            //        writeSucCount++;
            //    }
            //    else
            //    {
            //        writeFailedCount++;
            //    }
            //    string bb = "";
            //    CacheManger.Instance.TryGetValue(i.ToString(), out bb);
            //    if (string.IsNullOrEmpty(bb))
            //    {
            //        rFailedCount++;
            //    }
            //    else
            //    {
            //        rSucCount++;
            //    }
               
            //    //bool r2 = CacheManger.Instance.DeleteValue(i.ToString());
            //    //if (r2)
            //    //{
            //    //    dSucCount++;
            //    //}
            //    //else
            //    //{
            //    //    dFailedCount++;
            //    //}
            //    Console.WriteLine("set:{key:" + i.ToString() + ",value:" + "b" + i.ToString() + "},get:{key:" + i.ToString() + ",value:" + bb + "}" + "delete:");//+r2
            //    sum++;
            //    // Console.WriteLine("key:" + i.ToString() + ",value:" + bb);
            //}
            //wt.Stop();
            //Console.WriteLine("耗时:" + wt.ElapsedMilliseconds / 1000);
            //Console.WriteLine("写入成功率:" + writeSucCount / sum + ",写入失败率：" + writeFailedCount / sum);
            //Console.WriteLine("读取成功率:" + rSucCount / sum + ",读取失败率：" + rFailedCount / sum);
            //Console.WriteLine("删除成功率:" + dSucCount / sum + ",删除失败率：" + dFailedCount / sum);

            //Console.WriteLine("All Keys:");
            //CacheManger.Instance.GetAllKeys(CacheExpire.All).ForEach(x =>
            //{
            //    Console.WriteLine(x);
            //}); 
            #endregion


            #region 消息总线测试
            
            #endregion

            #region 初始化配置

            NdConfiguration.Create()//创建对象
                           .UseAutofac()//用autofac
                           .RegisterCommonComponents()//注册通用组件
                           .UseLog4Net()//替换默认的日志组件
                           .RegisterUnhandledExceptionHandler()//注册当前域下未经处理的异常
                           ;
            INDLogger logger=ObjectContainer.Resolve<INDLoggerFactory>().GetLogger(typeof(Program));      
           // int i = 2, j = 0;
                
            //int b=i / j;
            logger.Info("hello word3");
            #endregion

           

           


            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }

   
    
}
