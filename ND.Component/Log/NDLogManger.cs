using ND.Component.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NDLogManger.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/15 10:29:15         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/15 10:29:15          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log
{
   public class NDLogManger:INDLogManger
    {
       private static INDLoggerFactory _logFactory;
        private static readonly object _loadLock = new object();
        private static Func<MethodBase> _getCallingMethod;
        #region property
        public static INDLoggerFactory LogFactory
        {
            get
            {
                if (_logFactory == null)
                {
                    lock (_loadLock)
                    {
                        if (_logFactory == null)
                        {
                            _logFactory = BuildNDLoggerFactory();
                        }
                    }
                }
                return _logFactory;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Adapter");
                }

                lock (_loadLock)
                {
                    _logFactory = value;
                }
            }
        }
        INDLoggerFactory INDLogManger.LogFactory
        {
            get
            {
                return LogFactory;
            }
            set
            {
                LogFactory = value;
            }
        }
        #endregion

       
       /// <summary>
       /// 组件logfactory
       /// </summary>
       /// <returns></returns>
        private static INDLoggerFactory BuildNDLoggerFactory()
        {
            if(NDComponentConfig.Instance.LogProvider == null || string.IsNullOrEmpty(NDComponentConfig.Instance.LogProvider.Type) || NDComponentConfig.Instance.LogProvider.IsEnabled == false)
            {
                return new NullNDLoggerFactory();
            }
            string assemblyName=NDComponentConfig.Instance.LogProvider.Type.Split(',')[0];
            string typeName=NDComponentConfig.Instance.LogProvider.Type.Split(',')[1];
            string aa=NDComponentConfig.Instance.LogProvider.Type;
            Type type = Type.GetType(assemblyName+"."+typeName+","+assemblyName);
            return (INDLoggerFactory)Activator.CreateInstance(type);
        }
        #region Reset
        static NDLogManger()
        {
            Reset();
        }
        void INDLogManger.Reset()
        {
            Reset();
        }
        public static void Reset()
        {
            lock (_loadLock)
            {
                _logFactory = null;
            }
        }
        #endregion


        [Obsolete("Null-Reference Exception when dealing with Dynamic Types, Prefer instead one of the LogManager.GetLogger(...) variants.")]
        [MethodImpl(MethodImplOptions.NoInlining)]//指定如何实现方法的详细信息。 此类不能被继承 不能内联该方法。MethodImplOptions.NoInlining：内联是用方法主体代替方法调用的优化
        public INDLogger GetCurrentClassLogger()
        {
            var method = GetCallingMethod();
            var declaringType = method.DeclaringType;
            return LogFactory.GetLogger(declaringType);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static MethodBase GetCallingMethod()
        {
            Func<MethodBase> getCallingMethod = _getCallingMethod;
            if (getCallingMethod == null)
            {
                lock (_loadLock)
                {
                    if (_getCallingMethod == null)
                        _getCallingMethod = CreateGetClassNameFunction();

                    getCallingMethod = _getCallingMethod;
                }
            }
            return getCallingMethod();
        }
        private static Func<MethodBase> CreateGetClassNameFunction()
        {
            // Create and compile code similar to the following
            //var frame = new StackFrame(1, false);
            //var method = frame.GetMethod();
            //var declaringType = method.DeclaringType;
            var stackFrameType = Type.GetType("System.Diagnostics.StackFrame");
            if (stackFrameType == null)
                throw new PlatformNotSupportedException("CreateGetClassNameFunction is only supported on platforms where System.Diagnostics.StackFrame exist");

            var constructor = stackFrameType.GetConstructor(new[] { typeof(int) });
            var getMethodMethod = stackFrameType.GetMethod("GetMethod");

            if (constructor == null)
                throw new PlatformNotSupportedException("StackFrame(int skipFrames) constructor not present");
            if (getMethodMethod == null)
                throw new PlatformNotSupportedException("StackFrame.GetMethod() not present");

            //var frame = new StackFrame(3, false);
            var stackFrame = Expression.New(constructor,
                                                Expression.Constant(3));
            //var method = frame.GetMethod();
            var method = Expression.Call(stackFrame, getMethodMethod);

            //var declaringType = method.DeclaringType;
            var lambda = Expression.Lambda<Func<MethodBase>>(method);

            // Expression<TDelegate>.Compile  is missing in portable libraries targeting silverlight
            // but it is present on silverlight so we can just call it 
            //var function = lambda.Compile();
            var compileFunction = lambda.GetType().GetMethod("Compile", new Type[0]);
            var function = (Func<MethodBase>)compileFunction.Invoke(lambda, null);

            return function;
        }


        public INDLogger GetLogger<T>()
        {
            return LogFactory.GetLogger(typeof(T));
        }

        public INDLogger GetLogger(Type type)
        {
            return LogFactory.GetLogger(type.FullName);
        }

        public INDLogger GetLogger(string key)
        {
            return LogFactory.GetLogger(key);
        }


       

        
    }
}
