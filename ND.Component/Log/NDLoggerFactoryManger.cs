using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoggerFactory.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/18 10:41:14         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/18 10:41:14          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log
{
    /// <summary>
    /// ILoggerFactory
    /// </summary>
    public  class NDLoggerFactoryManger
    {
        public static NDLoggerFactoryManger Instance = null;
        public  NDFormatProvider formatProvider = new NDFormatProvider();
        private Type _type = null;
        static NDLoggerFactoryManger()
        {
            if(Instance==null)
            {
                Instance = new NDLoggerFactoryManger();
            }
        }
        //public NDLoggerFactoryManger LoadCurrentClassLogger(Type type)
        //{
        //    _type = type;
        //    return this;
        //}
        private NDLoggerFactoryManger() { }
        private static Dictionary<string, INDLoggerFactory> factorys = new Dictionary<string, INDLoggerFactory>();
        private static NDLoggerCollection loggerCollection = new NDLoggerCollection();
        private static event EventHandler<NDLogEventArgs> onLogging = null;
        private static void OnLogging(NDLogEventArgs args)
        {
            if(onLogging!=null)
            {
                onLogging(null, args);
            }
        }

       #region 添加日志提供者
        public static void AddFactory(LogCategory logCategory,INDLoggerFactory provider)
        {
            if(factorys.ContainsKey(logCategory.ToString()))
            {
                throw new ArgumentException("添加重复键值:"+logCategory.ToString());
            }
               factorys.Add(logCategory.ToString(),provider);
               INDLogger logger= provider.CreateLogger();
                 if(logger!=null)
                 {
                     loggerCollection.Add(logger); 
                 }
            
        }
        public static void AddFactory(string logCategory, INDLoggerFactory provider)
        {
            if (factorys.ContainsKey(logCategory))
            {
                throw new ArgumentException("添加重复键值:" + logCategory.ToString());
            }
            factorys.Add(logCategory, provider);
            INDLogger logger = provider.CreateLogger();
            if (logger != null)
            {
                loggerCollection.Add(logger);
            }

        } 
	    #endregion

       #region 添加日志提供者
        public static bool RemoveFactory(string logCategory)
       {
            if(!factorys.ContainsKey(logCategory.ToString()))
            {
                return false;
            }
            INDLogger logger= factorys[logCategory.ToString()].GetLogger();
            loggerCollection.Remove(logger);
            return true;
       }
       #endregion

       #region 获取所有的日志提供者
		  public static List<INDLoggerFactory> GetProviders()
        {
            return factorys.Values.ToList();
        } 
	#endregion

       #region 获取所有的logger
       public static List<INDLogger> GetLoggers()
        {
            return loggerCollection.ToList();
        } 
        #endregion


       #region  Log
       public void Log<T>(NDLogLevel logLevel,T message, Exception exception,IFormatProvider provider,params object[] args) where T:class
       {
               try
               {
                   //OnLogging(new NDLogEventArgs() { 
                   //     Args=args,
                   //     Exception=exception,
                   //     LogLevel=logLevel,
                   //     Message=message,
                   //     Provider=provider
                   //});
                   loggerCollection.ToList().ForEach(x =>
                   {
                       try
                       {
                           x.Log(logLevel, JsonConvert.SerializeObject(message), exception, provider, args);
                       }
                       catch(Exception ex)
                       {

                       }
                   });
                   
               }
               catch(Exception ex)
               {

               }
       }
       #endregion
    }
}
