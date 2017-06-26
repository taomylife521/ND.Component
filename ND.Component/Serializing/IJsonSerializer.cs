﻿//**********************************************************************
//
// 文件名称(File Name)：        
// 功能描述(Description)：     
// 作者(Author)：               
// 日期(Create Date)： 2017-06-26 9:50:47         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期: 2017-06-26 9:50:47           
//             修改理由：         
//
//       R2:
//             修改作者:          
//             修改日期:  2017-06-26 9:50:47         
//             修改理由：         
//
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.Component.Serializing
{
    /// <summary>
    /// 通用序列化接口
    /// </summary>
    public interface IJsonSerializer
    {
        string Serialize(object obj);
        object Deserialize(string value, Type type);
        T Deserialize<T>(string value) where T : class;
    }
}
