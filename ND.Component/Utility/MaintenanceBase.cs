using ND.Component.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：MaintenanceBase.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/20 14:34:54         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/20 14:34:54          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Utility
{
    public class MaintenanceBase : IDisposable
    {
      
      //  protected readonly INDLogger logger;
       public MaintenanceBase()
       {
          
       }

       public virtual void Dispose()
       {
           this.Dispose();
       }
    }
}
