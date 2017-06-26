using ND.Component.Components;
using ND.Component.LoadBalance;
using ND.Component.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.Component.Caching;
using ND.Component.Serializing;

//**********************************************************************
//
// 文件名称(File Name)：NDConfiguration.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-06-20 17:21:10         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-06-20 17:21:10          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Configurations
{
    public class NdConfiguration
    {
        /// <summary>
        /// 提供一个单例访问对象
        /// </summary>
        public static NdConfiguration Instance { get; private set; }

        private NdConfiguration() { }

        public static NdConfiguration Create()
        {
            Instance = new NdConfiguration();
            return Instance;
        }
        #region 注册默认集合对象
        /// <summary>
        /// 注册默认集合对象
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementer"></typeparam>
        /// <param name="serviceName"></param>
        /// <param name="life"></param>
        /// <returns></returns>
        public NdConfiguration SetDefault<TService, TImplementer>(string serviceName = null, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            ObjectContainer.Register<TService, TImplementer>(serviceName, life);
            return this;
        } 
        #endregion

        #region 注册默认集合对象
        /// <summary>
        /// 注册默认集合对象
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementer"></typeparam>
        /// <param name="instance"></param>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public NdConfiguration SetDefault<TService, TImplementer>(TImplementer instance, string serviceName = null)
            where TService : class
            where TImplementer : class, TService
        {
            ObjectContainer.RegisterInstance<TService, TImplementer>(instance, serviceName);
            return this;
        } 
        #endregion

     

        #region 注册通用组件

        public NdConfiguration RegisterCommonComponents()
        {
            SetDefault<INDLoggerFactory,NullNDLoggerFactory>();//注册默认日志组件
            SetDefault<IBalance, HashBalance>();//注册默认负载均衡组件
            SetDefault<ICache, InMemoryCache>();//注册默认缓存组件
            SetDefault<IJsonSerializer, NotImplementedJsonSerializer>();//注册默认序列化组件
            return this;

        }

        #endregion

        #region MyRegion
        public NdConfiguration RegisterUnhandledExceptionHandler()
        {
            var logger = ObjectContainer.Resolve<INDLoggerFactory>().GetLogger(GetType().FullName);
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => logger.Error("Unhandled exception: {0}", e.ExceptionObject);
            return this;
        } 
        #endregion
    }
}
