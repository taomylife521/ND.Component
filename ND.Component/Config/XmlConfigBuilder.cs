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
        }
        #region BuildCacheProvider
        private CacheConfigProvider BuildCacheProvider(XElement root)
        {
            CacheConfigProvider cacheProvider = null;
            XElement rootCache = root.Element("cacheprovider");
            if (rootCache != null)
            {
                cacheProvider = new CacheConfigProvider();
                cacheProvider.CacheDBName = rootCache.Attribute("dbname").Value;
                cacheProvider.CacheTableName = rootCache.Attribute("tablename").Value;
                foreach (var elm in rootCache.Elements("provider"))
                {
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
    }
}
