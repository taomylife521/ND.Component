using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NullNDLoggerFactory.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/15 11:03:50         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/15 11:03:50          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log
{
    public class NullNDLoggerFactory:INDLoggerFactory
    {
        private static readonly INDLogger _logger =new NullNDLogger();
       
        public INDLogger GetLogger(Type type)
        {
            return _logger;
        }

        public INDLogger GetLogger(string name)
        {
            return _logger;
        }
    }
}
