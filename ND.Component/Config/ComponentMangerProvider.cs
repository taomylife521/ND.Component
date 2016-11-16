
using Newtonsoft.Json;
//**********************************************************************
//
// 文件名称(File Name)：        PluginProvider.cs
// 功能描述(Description)：      请求基础类
// 作者(Author)：               张维刚
// 日期(Create Date)：          2015-12-26 20:36:48
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:          
//             修改理由：         
//
//       R2:
//             修改作者:          
//             修改日期:          
//             修改理由：         
//
//**********************************************************************
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//（暂无用）基础层组件管理器配置
namespace ND.Component.Config
{
    /// <summary>
    /// 插件宿主
    /// </summary>
    public   class ComponentMangerProvider
    {
       
        private static ConcurrentDictionary<string, string> _headers = null;

      

       
        internal static ConcurrentDictionary<string, string> Headers
        {
            get { return ComponentMangerProvider._headers; }
            private set { ComponentMangerProvider._headers = value; }
        }

        private static ConcurrentDictionary<string, Func<object>> _pluginCollection = null;//插件集合
        

       
        /// <summary>
        /// 插件集合
        /// </summary>
        public static ConcurrentDictionary<string, Func<object>> PluginCollection
        {
            get { return ComponentMangerProvider._pluginCollection; }
            private set { ComponentMangerProvider._pluginCollection = value; }
        }

        static ComponentMangerProvider()
        {
            Headers = new ConcurrentDictionary<string, string>();
            PluginCollection = new ConcurrentDictionary<string, Func<object>>();
            RefreshPlugins();
        }

        private ComponentMangerProvider()
        { }

        /// <summary>
        /// 刷新插件集合
        /// </summary>
        public static void RefreshPlugins()
        {
           // string[] files = System.IO.Directory.GetFiles(_binPath, "ND.API*.dll", System.IO.SearchOption.AllDirectories);
            try
            {
                string[] files = new string[] { "ND.Component, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" };
                List<Assembly> assemblies = new List<Assembly>();
                foreach (string file in files)
                {
                    try
                    {
                        Assembly assembly = Assembly.Load(file);
                        if (assembly != null)
                        {
                            assemblies.Add(assembly);
                        }
                    }
                    catch { }
                }
                ConcurrentDictionary<string, string> headers = new ConcurrentDictionary<string, string>();
                ConcurrentDictionary<string, Func<object>> collection = new ConcurrentDictionary<string, Func<object>>();

                foreach (var assembly in assemblies)
                {
                    //缓存插件对象
                    #region 缓存插件对象
                    try
                    {
                        var typeList = (from a in
                                            (from t in assembly.GetTypes()
                                             select new { TheType = t, Interfaces = t.GetInterfaces() })
                                        where a.Interfaces.Length >= 1 && a.Interfaces.Contains(typeof(IComponentManger))
                                        select a).ToList();


                        foreach (var t in typeList)
                        {
                            //Type type = t.Interfaces.FirstOrDefault(tp => { return tp != typeof(IPlugin); });
                            if (!t.TheType.IsAbstract && !t.TheType.IsGenericType)
                            {
                                ConstructorInfo constructorInfo = t.TheType.GetConstructor(Type.EmptyTypes);
                                if (constructorInfo != null)
                                {
                                    try
                                    {
                                        Func<object> func = Expression.Lambda<Func<object>>(Expression.New(constructorInfo)).Compile();
                                        headers.TryAdd(t.TheType.Name, t.TheType.FullName);
                                        collection.TryAdd(t.TheType.FullName, func);
                                    }
                                    catch (Exception ex) { }

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    #endregion

                }
                Headers = headers;
                PluginCollection = collection;
            }
            catch(Exception ex)
            {
               // log.Error("PluginProvider.RefreshPlugins:" + ex.Message + ",StackTrace:" + JsonConvert.SerializeObject(ex.StackTrace));
            }
           
        }


      

        #region 获取插件
        /// <summary>
        /// 获取插件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IComponentManger GetPlugin(string key)
        {
            Func<object> pluginConstructor = null;
            if (!PluginCollection.TryGetValue(key, out pluginConstructor))
            {
                string otherKey = key;
                if (Headers.TryGetValue(key, out otherKey))
                {
                    PluginCollection.TryGetValue(otherKey, out pluginConstructor);
                }
            }
            IComponentManger plugin = null;
            if (pluginConstructor != null)
            {
                plugin = pluginConstructor() as IComponentManger;
            }
            if(plugin == null)
            {
               throw new Exception("未找到key对应的任何组件!");
            }
            return plugin;
        } 
        #endregion

        #region 获取插件
        /// <summary>
        /// 获取插件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetCurrentPlugin<T>(string key)
        {
            IComponentManger plugin = null;
            plugin = ComponentMangerProvider.GetPlugin(key);
            T pluginResult = (T)plugin;
            return pluginResult;
        }
        #endregion

        #region 获取插件列表
        /// <summary>
        /// 获取插件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<string> GetPluginKeyList()
        {
            return PluginCollection.Keys.ToList();  
        }
        #endregion

        #region 获取插件列表
        /// <summary>
        /// 获取插件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool RefreshPluginConfig(string key)
        {
            if(!PluginCollection.ContainsKey(key))
            {
                return false;
            }
          // return ((IComponentManger)PluginCollection[key]).ResetMangerConfig();
            return false;
        }
        #endregion

    }
}
