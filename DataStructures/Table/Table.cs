using DataStructures.Iterator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Table
{
    public abstract class Table<T> : Iterable<T> where T : IComparable<T>
    {
        public abstract bool Add(T data);
        public abstract bool Delete(T data);
        public abstract T? Find(T data);
        public abstract Iterator<T> createIterator();        
    }
}
