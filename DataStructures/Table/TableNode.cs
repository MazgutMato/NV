using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Table
{
    public class TableNode<T>
    {
        public T? Data;

        public TableNode()
        {
            Data = default(T);
        }

        public TableNode(T data)
        {
            Data = data;
        }
    }
}
