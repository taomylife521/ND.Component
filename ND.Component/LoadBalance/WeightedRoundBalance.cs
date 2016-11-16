using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：WeightedRoundRobinScheduling.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/8 16:19:11         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/8 16:19:11          
//             修改理由：         
//**********************************************************************
namespace ND.Component.LoadBalance
{
    /// <summary>
    /// 动态权重轮询算法
    /// </summary>
    public class WeightedRoundBalance:IBalance
    {
        public Server ChooseServer(List<Server> serviceconfiglist, string key)
        {
             Server server = null;
             Server best = null;
             int total = 0;
                for (int i = 0, len = serviceconfiglist.Count(); i < len; i++)
                {
                 //当前服务器对象
                    server = serviceconfiglist[i];
 
                 //当前服务器已宕机，排除
                 if(server.Down){
                     continue;
                 }
 
                 server.CurrentWeight += server.EffectiveWeight;
                 total += server.EffectiveWeight;
             
                 if(server.EffectiveWeight < server.weight){
                     server.EffectiveWeight++;
                 }
             
                 if(best == null || server.CurrentWeight>best.CurrentWeight){
                     best = server;
                 }
             
             }
 
         if (best == null) {
             return null;
         }
 
         best.CurrentWeight -= total;
         best.CheckedDate = DateTime.Now;
         return best;
        }
    }
}
