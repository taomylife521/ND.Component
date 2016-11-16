using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NullBalance.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/16 17:24:51         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/16 17:24:51          
//             修改理由：         
//**********************************************************************
namespace ND.Component.LoadBalance
{
    public class RandomBalance:IBalance
    {
        private static Random rt = new Random();
        private static object obj = new object();
        public Server ChooseServer(List<Server> serviceconfiglist, string key)
        {
            lock (obj)
            {
                int index = rt.Next(0, serviceconfiglist.Count);
                return serviceconfiglist[index];
            }
        }
    }
}
