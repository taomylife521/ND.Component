using ND.Component.Caching;
using ND.Component.LoadBalance;
using ND.Component.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

//**********************************************************************
//
// 文件名称(File Name)：XmlComponentConfigBuilder.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/8 13:52:48         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/8 13:52:48          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Config
{
   public class XmlConfigBuilder:IConfigBuilder
    {
       /// <summary>
       /// 构建配置项
       /// </summary>
        public void Build()
        {
            
            XElement root = XElement.Load(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NDComponent.xml"));
            NDComponentConfig.Instance.IsThrowConfigException =Convert.ToBoolean(root.Attribute("isthrowconfigexception").Value.NotEmpty("true"));
            NDComponentConfig.Instance.BalanceProvider = BuildBlanceProvider(root); 
            NDComponentConfig.Instance.CacheProvider = BuildCacheProvider(root);
            NDComponentConfig.Instance.LogProvider = BuildLogConfigProvider(root);
            
        }

        //组建缓存
        #region BuildCacheProvider
        private CacheConfigProvider BuildCacheProvider(XElement root)
        {
            CacheConfigProvider cacheProvider = null;
            XElement elm = root.Element("cacheprovider");
            if (elm != null)
            {
                cacheProvider = new CacheConfigProvider();
                    cacheProvider.CacheDBName = elm.Attribute("dbname").Value;
                    cacheProvider.CacheTableName = elm.Attribute("tablename").Value;
                    cacheProvider.IsLogging = string.IsNullOrEmpty(elm.Attribute("islogging").NotNull("").Value) ? false : Convert.ToBoolean(elm.Attribute("islogging").NotNull("").Value);
                    cacheProvider.Name = elm.Attribute("name").Value.NotEmpty("");
                    cacheProvider.Type = elm.Attribute("type").Value.NotEmpty("");
                    cacheProvider.IsEnabled = Convert.ToBoolean(elm.Attribute("isenabled").Value.NotEmpty("false"));
                    foreach (var elmItem in elm.Elements("cacheitem"))
                    {
                        cacheProvider.CacheItem.Add(new CacheItem()
                        {
                            IsEnabled = Convert.ToBoolean(elmItem.Attribute("isenabled").Value.NotEmpty("false")),
                            ConnStr = elmItem.Attribute("connstr").Value.NotEmpty(""),
                            WeightValue = Convert.ToInt32(elmItem.Attribute("weightvalue").Value.NotEmpty("1"))
                        });
                    }
                    cacheProvider.Cache = BuildCache(cacheProvider);
                
            }
           
            return cacheProvider;
        } 
        #endregion

        #region BuildCache
        /// <summary>
        /// 组件BuildCache
        /// </summary>
        /// <returns></returns>
        private static ICache BuildCache(CacheConfigProvider provider)
        {
            try
            {
                if (provider == null || string.IsNullOrEmpty(provider.Type) || provider.IsEnabled == false)
                {
                    if (NDComponentConfig.Instance.IsThrowConfigException)
                    {
                        throw new ArgumentNullException("Invalid_Config_CacheProvider");
                    }
                    return new NullCache();
                }

                string assemblyName = provider.Type.Split(',')[0];
                string typeName = provider.Type.Split(',')[1];
                
                if (typeName == "ND.Component.Caching.InMemoryCache")
                {
                    return new InMemoryCache();
                }
                Type type = Type.GetType(typeName + "," + assemblyName);
                return (ICache)Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                if (NDComponentConfig.Instance.IsThrowConfigException)
                {
                    throw new ArgumentNullException("Invalid_Config_CacheProvider");
                }
                return new NullCache();
            }
        }
        #endregion

      
        //组建日志
        #region BuildLogConfigProvider
        public LogConfigProvider BuildLogConfigProvider(XElement root)
        {
            LogConfigProvider logProvider = null;
            XElement rootLog = root.Element("logprovider");
            if (rootLog != null)
            {
                logProvider = new LogConfigProvider();
                logProvider.Type = rootLog.Attribute("type").Value;
                logProvider.IsEnabled = Convert.ToBoolean(rootLog.Attribute("isenabled").Value.NotEmpty("false"));
                logProvider.LogFactory = BuildNDLoggerFactory(logProvider);
            }
            return logProvider;
        }
        #endregion

        #region BuildNDLoggerFactory
        /// <summary>
        /// 组件logfactory
        /// </summary>
        /// <returns></returns>
        private static INDLoggerFactory BuildNDLoggerFactory(LogConfigProvider logProvider)
        {
            try
            {
                if (logProvider == null || string.IsNullOrEmpty(logProvider.Type) || logProvider.IsEnabled == false)
                {
                    if (NDComponentConfig.Instance.IsThrowConfigException)
                    {
                        throw new ArgumentNullException("Invalid_Config_LogProvider");
                    }
                    return new NullNDLoggerFactory();
                }
                string assemblyName = logProvider.Type.Split(',')[0];
                string typeName = logProvider.Type.Split(',')[1];
                Type type = Type.GetType(typeName + "," + assemblyName);
                return (INDLoggerFactory)Activator.CreateInstance(type);
            }
            catch(Exception ex)
            {
                if (NDComponentConfig.Instance.IsThrowConfigException)
                {
                    throw new ArgumentNullException("Invalid_Config_LogProvider");
                }
                return new NullNDLoggerFactory();
            }
        }
        #endregion


       //组建负载均衡
        #region BuildBlanceProvider
        public BalanceConfigProvider BuildBlanceProvider(XElement root)
        {
            BalanceConfigProvider provider = null;
            XElement root2 = root.Element("balanceprovider");
            if (root2 != null)
            {
                provider = new BalanceConfigProvider();
                provider.Type = root2.Attribute("type").Value;
                provider.IsEnabled = Convert.ToBoolean(root2.Attribute("isenabled").Value.NotEmpty("false"));
                provider.Balance = BuildBalance(provider);
            }
            return provider;
        }
        
        #endregion

        #region BuildBalance
        /// <summary>
        /// 组件logfactory
        /// </summary>
        /// <returns></returns>
        private static IBalance BuildBalance(BalanceConfigProvider provider)
        {
            try
            {
                if (provider == null || string.IsNullOrEmpty(provider.Type) || provider.IsEnabled == false)
                {
                    if (NDComponentConfig.Instance.IsThrowConfigException)
                    {
                        throw new ArgumentNullException("Invalid_Config_BalanceProvider");
                    }
                    return new RandomBalance();
                }
                string assemblyName = provider.Type.Split(',')[0];
                string typeName = provider.Type.Split(',')[1];
                Type type = Type.GetType(typeName + "," + assemblyName);
                return (IBalance)Activator.CreateInstance(type);
            }
            catch(Exception ex)
            {
                if (NDComponentConfig.Instance.IsThrowConfigException)
                {
                    throw new ArgumentNullException("Invalid_Config_BalanceProvider");
                }
                return new RandomBalance();
            }
        }
        #endregion



      
    }
}
