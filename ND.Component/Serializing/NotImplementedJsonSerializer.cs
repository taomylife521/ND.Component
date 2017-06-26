using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NotImplementedJsonSerializer.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-06-26 9:51:42         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-06-26 9:51:42          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Serializing
{
    public class NotImplementedJsonSerializer:IJsonSerializer
    {
        public string Serialize(object obj)
        {
            throw new NotSupportedException(string.Format("{0} does not support serializing object.", GetType().FullName));
        }

        public object Deserialize(string value, Type type)
        {
            throw new NotSupportedException(string.Format("{0} does not support deserializing object.", GetType().FullName));
        }

        public T Deserialize<T>(string value) where T : class
        {
            throw new NotSupportedException(string.Format("{0} does not support deserializing object.", GetType().FullName));
        }
    }
}
