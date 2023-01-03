using DataStructures.Iterator;
using DataStructures.Table;
using DataStructures.Tree.Balance.Strategy;
using DataStructures.Tree.Binary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Tree.Balance
{
    public class BalanceTree<T> : Table<T> where T : IComparable<T>
    {
        public BSTree<T> BSTree { get; }
        private readonly BalancingStrategy<T> BalancingStrategy;

        public BalanceTree(BalancingStrategy<T> strategy)
        {
            this.BalancingStrategy = strategy;
            this.BSTree = new BSTree<T>();
        }
        public override bool Add(T data)
        {
            return this.BalancingStrategy.Add(data, this.BSTree);
        }

        public override Iterator<T> createIterator()
        {
            return new InOrderBSTIterator<T>(BSTree);
        }

        public override bool Delete(T data)
        {
            return this.BalancingStrategy.Delete(data, this.BSTree);
        }

        public override T? Find(T data)
        {
            return this.BSTree.Find(data);
        }
    }
}
