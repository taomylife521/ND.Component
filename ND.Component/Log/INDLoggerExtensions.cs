
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoggerExtensions.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/18 11:16:30         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/18 11:16:30          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log
{
    public static class INDLoggerExtensions
    {
        

        #region DEBUG
        //------------------------------------------DEBUG------------------------------------------//
        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="INDLogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Debug<T>(this INDLogger logger, T message, params object[] args) where T:class
        {
            Debug(logger, message, null, null, args);
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="INDLogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Debug<T>(this INDLogger logger, T message, Exception exception, params object[] args) where T : class
        {
            Debug(logger, message, exception, null, args);
        }

        public static void Debug(this INDLogger logger, Exception exception, IFormatProvider formatter)
        {
            Debug(logger, "", exception, formatter, null);
        }

        public static void Debug<T>(this INDLogger logger, T message, Exception exception, IFormatProvider formatter, params object[] args) where T : class
        {
            if (logger == null)
            {
                throw new ArgumentNullException(logger.ToString());
            }
           // if (formatter == null)
              //  throw new ArgumentNullException(logger.ToString());
            
                logger.Log(NDLogLevel.Debug, message, exception, formatter, args);
            
        } 
        #endregion

        #region Trace
        //------------------------------------------Trace------------------------------------------//
        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="INDLogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Trace<T>(this INDLogger logger, T message, params object[] args) where T : class
        {
            Trace(logger, message, null, null, args);
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="INDLogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Trace<T>(this INDLogger logger, T message, Exception exception, params object[] args) where T : class
        {
            Trace(logger, message, exception, null, args);
        }

        public static void Trace(this INDLogger logger, Exception exception, IFormatProvider formatter)
        {
            Trace(logger, "", exception, formatter, null);
        }

        public static void Trace<T>(this INDLogger logger, T message, Exception exception, IFormatProvider formatter, params object[] args) where T : class
        {
            if (logger == null)
            {
                throw new ArgumentNullException(logger.ToString());
            }
           // if (formatter == null)
              //  throw new ArgumentNullException(logger.ToString());
            
                logger.Log(NDLogLevel.Trace, message, exception, formatter, args);
            
        }
        #endregion

        #region Info
        //------------------------------------------Info------------------------------------------//
        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="INDLogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
         
        public static void Info<T>(this INDLogger logger, T message, params object[] args) where T : class
        {
            Info(logger, message, null, null, args);
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="INDLogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        
        public static void Info<T>(this INDLogger logger, T message, Exception exception, params object[] args) where T : class
        {
            Info(logger, message, exception, null, args);
        }
         
        public static void Info(this INDLogger logger, Exception exception, IFormatProvider formatter)
        {
            Info(logger, "", exception, formatter, null);
        }

        public static void Info<T>(this INDLogger logger, T message, Exception exception, IFormatProvider formatter, params object[] args) where T : class
        {
            if (logger == null)
            {
                throw new ArgumentNullException(logger.ToString());
            }
           

            logger.Log(NDLogLevel.Information, message, exception, formatter, args);
        }
        #endregion

        #region Warn
        //------------------------------------------Warn------------------------------------------//
        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="INDLogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Warn<T>(this INDLogger logger, T message, params object[] args) where T : class
        {
            Warn(logger, message, null, null, args);
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="INDLogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Warn<T>(this INDLogger logger, T message, Exception exception, params object[] args) where T : class
        {
            Warn(logger, message, exception, null, args);
        }

        public static void Warn(this INDLogger logger, Exception exception, IFormatProvider formatter)
        {
            Warn(logger, "", exception, formatter, null);
        }

        public static void Warn<T>(this INDLogger logger, T message, Exception exception, IFormatProvider formatter, params object[] args) where T : class
        {
            if (logger == null)
            {
                throw new ArgumentNullException(logger.ToString());
            }
           // if (formatter == null)
               // throw new ArgumentNullException(logger.ToString());

            logger.Log(NDLogLevel.Warning, message, exception, formatter, args);
        }
        #endregion

        #region Error
        //------------------------------------------Error------------------------------------------//
        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="INDLogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Error<T>(this INDLogger logger, T message, params object[] args) where T : class
        {
            Error(logger, message, null, null, args);
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="INDLogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Error<T>(this INDLogger logger, T message, Exception exception, params object[] args) where T : class
        {
            Error(logger, message, exception, null, args);
        }

        public static void Error(this INDLogger logger, Exception exception, IFormatProvider formatter)
        {
            Error(logger, "", exception, formatter, null);
        }

        public static void Error<T>(this INDLogger logger, T message, Exception exception, IFormatProvider formatter, params object[] args) where T : class
        {
            if (logger == null)
            {
                throw new ArgumentNullException(logger.ToString());
            }
           // if (formatter == null)
                //throw new ArgumentNullException(logger.ToString());

            logger.Log(NDLogLevel.Error, message, exception, formatter, args);
        }
        #endregion

        #region CRITICAL
        //------------------------------------------Error------------------------------------------//
        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="INDLogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Critical<T>(this INDLogger logger, T message, params object[] args) where T : class
        {
            Critical(logger, message, null, null, args);
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="INDLogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Critical<T>(this INDLogger logger, T message, Exception exception, params object[] args) where T : class
        {
            Critical(logger, message, exception, null, args);
        }

        public static void Critical(this INDLogger logger, Exception exception, IFormatProvider formatter)
        {
            Critical(logger, "", exception, formatter, null);
        }

        public static void Critical<T>(this INDLogger logger, T message, Exception exception, IFormatProvider formatter, params object[] args) where T : class
        {
            if (logger == null)
            {
                throw new ArgumentNullException(logger.ToString());
            }
           // if (formatter == null)
              //  throw new ArgumentNullException(logger.ToString());

            logger.Log(NDLogLevel.Critical, message, exception, formatter, args);
        }

        #endregion

       

      
    }
}
