using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：ObjectContainer.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-06-20 16:05:07         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-06-20 16:05:07          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Components
{
    public class ObjectContainer
    {
        public static IObjectContainer CurrentContainer { get; private set; }

        /// <summary>Set the object container.
        /// </summary>
        /// <param name="container"></param>
        public static void SetContainer(IObjectContainer container)
        {
            CurrentContainer = container;
        }

        public static void RegisterType(Type implementationType, string serviceName = null, LifeStyle life = LifeStyle.Singleton)
        {
            CurrentContainer.RegisterType(implementationType,serviceName,life);
        }

        public static void RegisterType(Type serviceType, Type implementationType, string serviceName = null, LifeStyle life = LifeStyle.Singleton)
        {
            CurrentContainer.RegisterType(serviceType,implementationType, serviceName, life);
        }

        public static void Register<TService, TImplementer>(string serviceName = null, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            CurrentContainer.Register<TService, TImplementer>(serviceName, life);
        }

        public static void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null)
            where TService : class
            where TImplementer : class, TService
        {
            CurrentContainer.RegisterInstance<TService, TImplementer>(instance, serviceName);
        }

        public static TService Resolve<TService>() where TService : class
        {
            return CurrentContainer.Resolve<TService>();
        }

        public static object Resolve(Type serviceType)
        {
            return CurrentContainer.Resolve(serviceType);
        }

        public static bool TryResolve<TService>(out TService instance) where TService : class
        {
            return CurrentContainer.TryResolve<TService>(out instance);
        }

        public static bool TryResolve(Type serviceType, out object instance)
        {
            return CurrentContainer.TryResolve(serviceType,out instance);
        }

        public static TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            return CurrentContainer.ResolveNamed<TService>(serviceName);
        }

        public static object ResolveNamed(string serviceName, Type serviceType)
        {
            return CurrentContainer.ResolveNamed(serviceName,serviceType);
        }

        public static bool TryResolveNamed(string serviceName, Type serviceType, out object instance)
        {
            return CurrentContainer.TryResolveNamed(serviceName, serviceType,out instance);
        }
    }
}
