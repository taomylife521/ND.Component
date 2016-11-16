﻿using ND.Component.LoadBalance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：BalanceConfigProvider.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/16 14:18:50         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/16 14:18:50          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Config
{
    public class BalanceConfigProvider : ConfigProviderBase
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 负载均衡类
        /// </summary>
        public IBalance Balance { get; set; }
    }
}
