﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：Log4NetLoggerFactory.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/20 14:01:02         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/20 14:01:02          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log.Log4Net
{
    public class Log4NetLoggerFactory : INDLoggerFactory
    {
        private Log4NetLogger logger = null;
        public INDLogger CreateLogger()
        {
            logger = new Log4NetLogger();
            return logger;
        }

        public INDLogger GetLogger()
        {
            return logger;
        }
    }
}
