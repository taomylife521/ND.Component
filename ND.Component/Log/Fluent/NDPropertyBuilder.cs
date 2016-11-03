using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：PropertyBuilder.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/18 11:58:21         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/18 11:58:21          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log.Fluent
{
    public class NDPropertyBuilder
    {
        public IDictionary<string, object> Properties { get{return new Dictionary<string,object>();} }

        public NDPropertyBuilder Property(string name, object value) {
            if (name == null)
                throw new ArgumentNullException(name.ToString());

            Properties[name] = value;
            return this;
        }
    }
}
