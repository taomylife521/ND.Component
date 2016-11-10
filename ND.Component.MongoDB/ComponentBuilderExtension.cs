using ND.Component.Caching;
using ND.Component.ComponentBuilder;
using ND.Component.MongoDB.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：ComponentBuilderExtension.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/9 17:01:16         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/9 17:01:16          
//             修改理由：         
//**********************************************************************
namespace ND.Component.MongoDB
{
   public static class ComponentBuilderExtension
    {
       public static IComponentBuilder UseMongoDBCache(this IComponentBuilder build)
       {
           CacheManger.InitCache(new MongoDBCache());
           return build;
       }
    }
}
