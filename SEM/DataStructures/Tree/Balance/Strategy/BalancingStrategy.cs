using DataStructures.Tree.Binary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Tree.Balance.Strategy
{
    public abstract class BalancingStrategy<T> where T : IComparable<T>
    {
        public bool RotateRight(BSTNode<T> node, BSTree<T> tree)
        {
            if (node != null)
            {
                if (node.LeftNode == null)
                {
                    return false;
                }
                var leftNode = node.LeftNode;

                if (node.Parent != null)
                {
                    leftNode.Parent = node.Parent;
                    if (leftNode.Data.CompareTo(leftNode.Parent.Data) < 0)
                    {
                        leftNode.Parent.LeftNode = leftNode;
                    }
                    else
                    {
                        leftNode.Parent.RightNode = leftNode;
                    }
                }
                else
                {
                    leftNode.Parent = null;
                    tree.Root = leftNode;
                }

                if (leftNode.RightNode != null)
                {
                    node.LeftNode = leftNode.RightNode;
                    node.LeftNode.Parent = node;
                }
                else
                {
                    node.LeftNode = null;
                }

                node.Parent = leftNode;
                leftNode.RightNode = node;

                return true;
            }
            return false;
        }
        public bool RotateLeft(BSTNode<T> node, BSTree<T> tree)
        {
            if (node != null)
            {
                if (node.RightNode == null)
                {
                    return false;
                }
                var rightNode = node.RightNode;

                if (node.Parent != null)
                {
                    rightNode.Parent = node.Parent;
                    if (rightNode.Data.CompareTo(rightNode.Parent.Data) < 0)
                    {
                        rightNode.Parent.LeftNode = rightNode;
                    }
                    else
                    {
                        rightNode.Parent.RightNode = rightNode;
                    }
                }
                else
                {
                    rightNode.Parent = null;
                    tree.Root = rightNode;
                }

                if (rightNode.LeftNode != null)
                {
                    node.RightNode = rightNode.LeftNode;
                    node.RightNode.Parent = node;
                }
                else
                {
                    node.RightNode = null;
                }

                node.Parent = rightNode;
                rightNode.LeftNode = node;
            }

            return false;
        }
        public abstract bool Add(T data, BSTree<T> tree);
        public abstract bool Delete(T data, BSTree<T> tree);
    }
}
