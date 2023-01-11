using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Iterator
{
    public interface Iterable<T> where T : IComparable<T>
    {
        Iterator<T> createIterator();
    }
}
