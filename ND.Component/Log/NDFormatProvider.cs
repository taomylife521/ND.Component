using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NDFormatProvider.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/20 10:56:38         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/20 10:56:38          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log
{
    public class NDFormatProvider : IFormatProvider
    {
        public object GetFormat(Type formatType)
        {
            return formatType;
        }
    }
}
