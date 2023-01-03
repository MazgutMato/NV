using DataStructures.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructures.Tree.Binary
{
    public class BSTNode<T> : TableNode<T> where T : IComparable<T>
    {
        public BSTNode<T>? LeftNode { get; set; }
        public BSTNode<T>? RightNode { get; set; }
        public BSTNode<T>? Parent { get; set; }

        public BSTNode()
        {
            LeftNode = null;
            RightNode = null;
            Parent = null;
        }
        public BSTNode(T data)
        {
            Data = data;
            LeftNode = null;
            RightNode = null;
            Parent = null;
        }
        public BSTNode(T data, BSTNode<T> parent)
        {
            Data = data;
            LeftNode = null;
            RightNode = null;
            Parent = parent;
        }

        public bool IsLeaf()
        {
            if (RightNode == null && LeftNode == null)
            {
                return true;
            }
            return false;
        }
    }
}