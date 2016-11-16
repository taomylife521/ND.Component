//**********************************************************************
//
// 文件名称(File Name)：        
// 功能描述(Description)：     
// 作者(Author)：               
// 日期(Create Date)： 2016/11/15 10:27:40         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期: 2016/11/15 10:27:40           
//             修改理由：         
//
//       R2:
//             修改作者:          
//             修改日期:  2016/11/15 10:27:40         
//             修改理由：         
//
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.Component.Log
{
    public interface INDLogManger
    {

        INDLoggerFactory LogFactory { get;  }

     

        INDLogger GetCurrentClassLogger();

        INDLogger GetLogger<T>();

        INDLogger GetLogger(Type type);

        INDLogger GetLogger(string key);
    }
}
