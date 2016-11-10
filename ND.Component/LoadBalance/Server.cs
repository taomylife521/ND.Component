using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：Server.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/8 16:13:02         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/8 16:13:02          
//             修改理由：         
//**********************************************************************
namespace ND.Component.LoadBalance
{
    public class Server
    {
          private bool down = false;
          private string ip = "";
          public int weight = 1;
          public string connstr = "";
          public String Ip { get { return ip; } set { ip = value; } }

          public int Weight { get { return weight; } set { weight = value; } }

        /// <summary>
          /// 由于每台机器的配置或响应能力不一样，所以需要给其一个权值，确保高配置或响应快的机器多分配到请求。
        /// </summary>
          public int EffectiveWeight { get; set; }

        /// <summary>
          /// 用于记录本次请求时服务器参与计算后的的权值，这样才能保证每次请求都不是同一台机器
        /// </summary>
          public int CurrentWeight { get; set; }

          public bool Down { get { return down; } set { down = value; } }

          public DateTime CheckedDate { get; set; }

          public string ConnStr { get { return connstr; } set { connstr = value; } }

         public Server(String _ip, int _weight,string connStr) {
            ip = _ip;
            weight = _weight;
            connstr = connStr;
            this.EffectiveWeight = this.Weight;
            this.CurrentWeight = 0;
            if(this.Weight < 0){
                this.Down = true;
            }else{
                this.Down = false;
            }
        }
    }
}
