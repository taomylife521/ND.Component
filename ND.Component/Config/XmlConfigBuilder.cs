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
            NDComponentConfig.Instance.CacheProvider = BuildCacheProvider(root);
            NDComponentConfig.Instance.LogProvider = BuildLogConfigProvider(root);
        }
        #region BuildCacheProvider
        private CacheConfigProvider BuildCacheProvider(XElement root)
        {
            CacheConfigProvider cacheProvider = null;
            XElement rootCache = root.Element("cacheprovider");
            if (rootCache != null)
            {
                cacheProvider = new CacheConfigProvider();
               
                foreach (var elm in rootCache.Elements("provider"))
                {
                    cacheProvider.CacheDBName = elm.Attribute("dbname").Value;
                    cacheProvider.CacheTableName = elm.Attribute("tablename").Value;
                    cacheProvider.IsLogging = string.IsNullOrEmpty(elm.Attribute("islogging").NotNull("").Value) ? false : Convert.ToBoolean(elm.Attribute("islogging").NotNull("").Value);
                    cacheProvider.Name = elm.Attribute("name").Value.NotEmpty("");
                    cacheProvider.IsEnabled = Convert.ToBoolean(elm.Attribute("isenabled").Value.NotEmpty("false"));
                    foreach (var elmItem in elm.Elements("provideritem"))
                    {
                        cacheProvider.CacheItem.Add(new CacheProviderItem()
                        {
                            IsEnabled = Convert.ToBoolean(elmItem.Attribute("isenabled").Value.NotEmpty("false")),
                            ConnStr = elmItem.Attribute("connstr").Value.NotEmpty(""),
                            WeightValue = Convert.ToInt32(elmItem.Attribute("weightvalue").Value.NotEmpty("1"))
                        });
                    }
                }
            }
            return cacheProvider;
        } 
        #endregion

        #region BuildLogConfigProvider
        public LogConfigProvider BuildLogConfigProvider(XElement root)
        {
            LogConfigProvider logProvider = null;
            XElement rootLog = root.Element("logprovider");
            if (rootLog != null)
            {
                rootLog = rootLog.Element("logfactory");
                logProvider = new LogConfigProvider();
                logProvider.Type = rootLog.Attribute("type").Value;
                logProvider.IsEnabled = Convert.ToBoolean(rootLog.Attribute("isenabled").Value.NotEmpty("false"));
            }
            return logProvider;
        }
        #endregion


        public void RefreshLogConfigProvider()
        {
            XElement root = XElement.Load(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NDComponent.xml"));
            NDComponentConfig.Instance.LogProvider = BuildLogConfigProvider(root);
        }

        public void RefreshCacheConfigProvider()
        {
            XElement root = XElement.Load(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NDComponent.xml"));
            NDComponentConfig.Instance.CacheProvider = BuildCacheProvider(root);
        }
    }
}
