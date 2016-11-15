using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AbsNDLoggerFactory.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/15 15:31:29         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/15 15:31:29          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log
{
    public abstract class AbsNDLoggerFactory:INDLoggerFactory,IDisposable
    {
        private readonly Dictionary<string, INDLogger> _cachedLoggers;

        protected AbsNDLoggerFactory()
            : this(true)
        {}

        protected AbsNDLoggerFactory(bool caseSensitiveLoggerCache)
        {
            _cachedLoggers = (caseSensitiveLoggerCache)
                                 ? new Dictionary<string, INDLogger>()
                                 : new Dictionary<string, INDLogger>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        ///CreateLogger 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected abstract INDLogger CreateLogger(string name);

        protected abstract INDLogger CreateLogger(Type type);

        /// <summary>
        /// 清除logger缓存
        /// </summary>
        protected void ClearLoggerCache()
        {
            lock (_cachedLoggers)
            {
                _cachedLoggers.Clear();
            }
        }

        /// <summary>
        /// GetLogger By Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public INDLogger GetLogger(Type type)
        {
            return GetLoggerInternal(type.FullName);
        }

       
        /// <summary>
        /// Get Logger by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public INDLogger GetLogger(string name)
        {
            return GetLoggerInternal(name);
        }

        #region GetLoggerInternal
        private INDLogger GetLoggerInternal(string key)
        {
            INDLogger log;
            if (!_cachedLoggers.TryGetValue(key, out log))
            {
                lock (_cachedLoggers)
                {
                    if (!_cachedLoggers.TryGetValue(key, out log))
                    {
                        log = CreateLogger(key);
                        if (log == null)
                        {
                            throw new ArgumentException(string.Format("{0} returned null on creating logger instance for key {1}", this.GetType().FullName, key));
                        }
                        _cachedLoggers.Add(key, log);
                    }
                }
            }
            return log;
        } 
        #endregion

        public void Dispose()
        {
            this._cachedLoggers.Clear();
        }
    }
}
