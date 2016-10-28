using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NDLoggerCollection.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/19 18:08:11         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/19 18:08:11          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Log
{
    public class NDLoggerCollection:IList<INDLogger>
    {
        private readonly List<INDLogger> logger = new List<INDLogger>();
        public int IndexOf(INDLogger item)
        {
            return logger.IndexOf(item);
        }

        public void Insert(int index, INDLogger item)
        {
            logger.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            logger.RemoveAt(index);
        }

        public INDLogger this[int index]
        {
            get
            {
                return logger[index];
            }
            set
            {
                logger[index] = value;
            }
        }

        public void Add(INDLogger item)
        {
            logger.Add(item);
        }

        public void Clear()
        {
            logger.Clear();
        }

        public bool Contains(INDLogger item)
        {
            return logger.Contains(item);
        }

        public void CopyTo(INDLogger[] array, int arrayIndex)
        {
            logger.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return logger.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(INDLogger item)
        {
            return logger.Remove(item);
        }

        public IEnumerator<INDLogger> GetEnumerator()
        {
            return logger.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
