using ND.Component.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：DefaultComponentBuilder.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/9 16:59:09         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/9 16:59:09          
//             修改理由：         
//**********************************************************************
namespace ND.Component.ComponentBuilder
{
    public class DefaultComponentBuilder : IComponentBuilder
    {
        public IComponentBuilder Build()
        {
            ConfigComponentFactory.Init(new XmlConfigBuilder());
            return this;
        }
    }
}
