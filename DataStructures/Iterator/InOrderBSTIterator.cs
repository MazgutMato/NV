using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructures.Tree.Binary;

namespace DataStructures.Iterator
{
    public class InOrderBSTIterator<T> : Iterator<T> where T : IComparable<T>
    {
        private Queue<BSTNode<T>> Path;

        public InOrderBSTIterator(BSTree<T> tree)
        {
            Path = new Queue<BSTNode<T>>();
            if(tree != null)
            {
                GenerateInorderPath(tree.Root);
            }            
        }
        private void GenerateInorderPath(BSTNode<T>? root)
        {
            BSTNode<T>? current = root;
            Stack<BSTNode<T>> stack = new Stack<BSTNode<T>>();

            if (current == null)
            {
                return;
            }

            while (current != null || stack.Count > 0)
            {
                while(current != null)
                {
                    stack.Push(current);
                    current = current.LeftNode;
                }
                current = stack.Pop();
                Path.Enqueue(current);
                current = current.RightNode;
            }
        }        
        public T? MoveNext()
        {
            return Path.Dequeue().Data;
        }

        public bool HasNext()
        {
            return Path.Count > 0 ? true : false;
        }
    }
}
