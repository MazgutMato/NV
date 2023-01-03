using DataStructures.Iterator;
using DataStructures.Table;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DataStructures.Tree.Binary
{
    public class BSTree<T> : Table<T> where T : IComparable<T>
    {
        public BSTNode<T>? Root { get; set; }
        public int Count { get; set; }
        public override Iterator<T> createIterator()
        {
            return new InOrderBSTIterator<T>(this);
        }           
        public override bool Add(T data)
        {
            BSTNode<T> Parent = null;
            var Child = Root;

            while (Child != null)
            {
                Parent = Child;
                int compResult = data.CompareTo(Parent.Data);

                if (compResult == 1)
                {
                    Child = Parent.RightNode;
                }
                else if (compResult == -1)
                {
                    Child = Parent.LeftNode;
                }
                else
                {
                    //Found same Node
                    return false;
                }
            }

            if (Parent == null)
            {
                Root = new BSTNode<T>(data);
            }
            else
            {
                var newPosition = data.CompareTo(Parent.Data);
                if (newPosition == 1)
                {
                    Parent.RightNode = new BSTNode<T>(data, Parent);
                }
                else
                {
                    Parent.LeftNode = new BSTNode<T>(data, Parent);
                }
            }

            Count++;
            return true;
        }
        public override T? Find(T data)
        {
            var findNode = Root;

            while (findNode != null)
            {
                int compResult = data.CompareTo(findNode.Data);
                if (compResult == 0)
                {
                    return findNode.Data;
                }
                else if (compResult == 1)
                {
                    findNode = findNode.RightNode;
                }
                else
                {
                    findNode = findNode.LeftNode;
                }
            }

            return default(T);
        }
        public bool FindRange(T min, T max, ICollection<T> structure)
        {
            return FindNodeRange(Root, min, max, structure);
        }
        private bool FindNodeRange(BSTNode<T> node, T min, T max, ICollection<T> structure)
        {
            BSTNode<T> current = Root;
            Stack<BSTNode<T>> stack = new Stack<BSTNode<T>>();

            if (current == null)
            {
                return false;
            }

            stack.Push(current);
            current = current.LeftNode;
            while (current != null || stack.Count > 0)
            {
                while (current != null)
                {
                    if (current.Data.CompareTo(min) >= 0)
                    {
                        stack.Push(current);
                        current = current.LeftNode;
                    }
                    else
                    {
                        if (current.RightNode != null)
                        {
                            stack.Push(current.RightNode);
                        }
                        current = null;
                    }
                }
                while (current == null && stack.Count > 0)
                {
                    current = stack.Pop();
                    if (current.Data.CompareTo(max) <= 0 && current.Data.CompareTo(min) >= 0)
                    {
                        structure.Add(current.Data);
                    }
                    current = current.RightNode;
                }
            }

            return true;
        }
        public override bool Delete(T data)
        {
            var Child = Root;
            BSTNode<T> removeNode = null;

            while (Child != null && removeNode == null)
            {
                int compResult = data.CompareTo(Child.Data);
                if (compResult == 0)
                {
                    removeNode = Child;
                }
                else if (compResult == 1)
                {
                    Child = Child.RightNode;
                }
                else
                {
                    Child = Child.LeftNode;
                }
            }

            //Dont found removed Node
            if (removeNode == null)
            {
                return false;
            }
            //Removed leaf Node
            if (removeNode.IsLeaf())
            {
                //Removed Node is Root
                if (removeNode.Parent == null)
                {
                    Root = null;
                    Count--;
                    return true;
                }
                //Removed from parent
                var compResult = removeNode.Data.CompareTo(removeNode.Parent.Data);
                if (compResult == -1)
                {
                    ((BSTNode<T>)removeNode.Parent).LeftNode = null;
                    Count--;
                    return true;
                }
                else if (compResult == 1)
                {
                    ((BSTNode<T>)removeNode.Parent).RightNode = null;
                    Count--;
                    return true;
                }
                return false;
            }
            //Removed node has one child
            if (removeNode.LeftNode == null)
            {
                //Removed Node is Root
                if (removeNode.Parent == null)
                {
                    Root = removeNode.RightNode;
                    Root.Parent = null;
                    Count--;
                    return true;
                }
                //Is right or left son of parent
                var compResult = removeNode.Data.CompareTo(removeNode.Parent.Data);
                if (compResult == -1)
                {
                    ((BSTNode<T>)removeNode.Parent).LeftNode = removeNode.RightNode;
                    removeNode.RightNode.Parent = removeNode.Parent;
                }
                else if (compResult == 1)
                {
                    ((BSTNode<T>)removeNode.Parent).RightNode = removeNode.RightNode;
                    removeNode.RightNode.Parent = removeNode.Parent;
                }
                Count--;
                return true;
            }
            if (removeNode.RightNode == null)
            {
                if (removeNode.Parent == null)
                {
                    Root = removeNode.LeftNode;
                    Root.Parent = null;
                    Count--;
                    return true;
                }
                //Is right or left son of parent
                var compResult = removeNode.Data.CompareTo(removeNode.Parent.Data);
                if (compResult == -1)
                {
                    ((BSTNode<T>)removeNode.Parent).LeftNode = removeNode.LeftNode;
                    removeNode.LeftNode.Parent = removeNode.Parent;
                }
                else if (compResult == 1)
                {
                    ((BSTNode<T>)removeNode.Parent).RightNode = removeNode.LeftNode;
                    removeNode.LeftNode.Parent = removeNode.Parent;
                }
                Count--;
                return true;
            }
            //Else search inorderSuccessor
            var inorderSuccessor = removeNode.RightNode;
            var leftChild = removeNode.RightNode;
            while (leftChild != null)
            {
                inorderSuccessor = leftChild;
                leftChild = leftChild.LeftNode;
            }
            //Change removeNode to inorderSuccessor
            removeNode.Data = inorderSuccessor.Data;
            //Inorder successor is rightNode
            if (removeNode.Data.CompareTo(removeNode.RightNode.Data) == 0)
            {
                if (inorderSuccessor.IsLeaf())
                {
                    removeNode.RightNode = null;
                    Count--;
                    return true;
                }
                //Has right child
                removeNode.RightNode = inorderSuccessor.RightNode;
                removeNode.RightNode.Parent = removeNode;
                Count--;
                return true;
            }
            //Inorder succesor is leaf
            if (inorderSuccessor.IsLeaf())
            {
                //Removed Node is Root
                if (inorderSuccessor.Parent == null)
                {
                    Root = null;
                    Count--;
                    return true;
                }
                //Removed from parent
                var compResult = inorderSuccessor.Data.CompareTo(inorderSuccessor.Parent.Data);
                if (compResult == -1)
                {
                    ((BSTNode<T>)inorderSuccessor.Parent).LeftNode = null;
                    Count--;
                    return true;
                }
                else if (compResult == 1)
                {
                    ((BSTNode<T>)inorderSuccessor.Parent).RightNode = null;
                    Count--;
                    return true;
                }
                return false;
            }
            //Inorder succesor has right child
            inorderSuccessor.RightNode.Parent = inorderSuccessor.Parent;
            ((BSTNode<T>)inorderSuccessor.Parent).LeftNode = inorderSuccessor.RightNode;
            Count--;
            return true;
        }
        public void Clear()
        {
            Root = null;
            Count = 0;
        }
    }
}