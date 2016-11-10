using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：HashBalance.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/8 15:43:48         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/8 15:43:48          
//             修改理由：         
//**********************************************************************
namespace ND.Component.LoadBalance
{
    public class HashBalance : IBalance
    {
        public Server ChooseServer(List<Server> serviceconfiglist, string key)
       {
            if(serviceconfiglist.Count ==1)//如果只有一个则返回
            {
                return serviceconfiglist[0];
            }
           if (serviceconfiglist == null || serviceconfiglist.Count == 0)
               return null;
           return serviceconfiglist[Math.Abs(key.GetHashCode()) % serviceconfiglist.Count];
       }
    }
}
