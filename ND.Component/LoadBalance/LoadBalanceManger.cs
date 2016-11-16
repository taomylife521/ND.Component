using ND.Component.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadBalanceFactory.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/8 15:45:36         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/8 15:45:36          
//             修改理由：         
//**********************************************************************
namespace ND.Component.LoadBalance
{
    /// <summary>
    /// 负责均衡工厂
    /// </summary>
    public class LoadBalanceManger  
    {
        private  IBalance _balance = NDComponentConfig.Instance.BalanceProvider.Balance;

        private static LoadBalanceManger instance = null;
        public static LoadBalanceManger Instance { get { return instance; } set { instance = value; } }
        private LoadBalanceManger()
        {

        }
        static LoadBalanceManger()
        {
            if(instance == null)
            {
                instance = new LoadBalanceManger();
            }
        }
      
      
        public  string ChooseServerConnStr(List<Server> serviceconfiglist, string key)
        {
            
            return _balance.ChooseServer(serviceconfiglist, key).ConnStr;
        }

        public  Server ChooseServer(List<Server> serviceconfiglist, string key)
        {
            return _balance.ChooseServer(serviceconfiglist, key);
        }

       
    }
}
