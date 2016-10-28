using ND.Component.Log.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：Logger.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/18 10:42:02         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/18 10:42:02          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log
{
    /// <summary>
    /// 抽象日志记录类
    /// </summary>
    public abstract class AbsNDLogger : INDLogger
    {

      
        private NDLogLevel _minLogLevel=NDLogLevel.Trace;
        private NDLogLevel defaultLogLevel=NDLogLevel.Information;
      
      
         public NDLogLevel DefaultLogLevel { get{return defaultLogLevel;} set{defaultLogLevel=value;} }
         public NDLogLevel MinLogLevel { get { return _minLogLevel; } set { _minLogLevel = value; } }
        public AbsNDLogger()
        {
           
            
        }
        
       #region Log
		 /// <summary>
        /// 记录日志
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public abstract void Log<T>(NDLogLevel logLevel, T message, Exception exception, IFormatProvider provider, params object[] args) where T:class;
      
	#endregion

       #region 查看是否启用日志级别
		/// <summary>
        /// 是否启用日志级别
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public virtual bool IsEnabled(NDLogLevel logLevel)
        {
            return true;
        } 
	#endregion

      
      


       #region 更改最小的日志记录级别
		  /// <summary>
        /// 更改最小的日志记录级别
        /// </summary>
        /// <param name="minLogLevel"></param>
        internal void ChangeMinLogLevel(NDLogLevel minLogLevel) {
            _minLogLevel = minLogLevel;
        } 
	#endregion

    }





 
       
    

     
}
