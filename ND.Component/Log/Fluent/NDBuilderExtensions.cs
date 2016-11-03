using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：BuilderExtensions.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/18 11:59:30         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/18 11:59:30          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log.Fluent
{
   public static class NDBuilderExtensions
    {
        public static INDLogBuilder Level(this INDLogger logger, NDLogLevel logLevel)
        {
            if (!logger.IsEnabled(logLevel))
                return NDNullLogBuilder.Instance;

            return new NDLogBuilder(logLevel, logger);
        }

        public static INDLogBuilder Trace(this INDLogger logger)
        {
            if (!logger.IsEnabled(NDLogLevel.Trace))
                return NDNullLogBuilder.Instance;

            return new NDLogBuilder(NDLogLevel.Trace, logger);
        }

        public static INDLogBuilder Debug(this INDLogger logger)
        {
            if (!logger.IsEnabled(NDLogLevel.Debug))
                return NDNullLogBuilder.Instance;

            return new NDLogBuilder(NDLogLevel.Debug, logger);
        }

        public static INDLogBuilder Info(this INDLogger logger)
        {
            if (!logger.IsEnabled(NDLogLevel.Information))
                return NDNullLogBuilder.Instance;

            return new NDLogBuilder(NDLogLevel.Information, logger);
        }

        public static INDLogBuilder Warn(this INDLogger logger)
        {
            if (!logger.IsEnabled(NDLogLevel.Warning))
                return NDNullLogBuilder.Instance;

            return new NDLogBuilder(NDLogLevel.Warning, logger);
        }

        public static INDLogBuilder Error(this INDLogger logger)
        {
            if (!logger.IsEnabled(NDLogLevel.Error))
                return NDNullLogBuilder.Instance;

            return new NDLogBuilder(NDLogLevel.Error, logger);
        }

        public static INDLogBuilder Critical(this INDLogger logger)
        {
            if (!logger.IsEnabled(NDLogLevel.Critical))
                return NDNullLogBuilder.Instance;

            return new NDLogBuilder(NDLogLevel.Critical, logger);
        }
    }
}
