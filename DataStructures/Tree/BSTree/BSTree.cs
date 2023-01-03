using DataStructures.Iterator;
using DataStructures.Table;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DataStructures.Tree.BSTree
{
    public class BSTree<T> : Table<T> where T : IComparable<T>
    {
        public BSTNode<T>? Root { get; set; }
        public int Count { get; set; }
        public override Iterator<T> createIterator()
        {
            return new InOrderBSTIterator<T>(this);
        }
        private BSTNode<T> AddData(T data)
        {
            BSTNode<T>? Parent = null;
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
                    return null;
                }
            }

            var newNode = new BSTNode<T>(data);
            if (Parent == null)
            {
                Root = newNode;
            }
            else
            {
                newNode.Parent = Parent;
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
            return newNode;
        }
        private void ModifyParents(BSTNode<T> node)
        {
            if (node == null)
            {
                return;
            }
            var Parent = (BSTNode<T>)node.Parent;
            //Random rotate if not balance
            while (Parent != null)
            {
                if (Parent.LeftNode != null && Parent.RightNode != null)
                {
                    Random random = new Random();
                    if (random.Next(0, 1) == 0)
                    {
                        this.RotateLeft(Parent);
                    }
                    else
                    {
                        this.RotateRight(Parent);
                    }
                    Parent = (BSTNode<T>)((Parent.Parent).Parent);
                }
                else if (Parent.LeftNode == null)
                {
                    this.RotateLeft(Parent);
                    Parent = (BSTNode<T>)((Parent.Parent).Parent);
                }
                else if (Parent.RightNode == null)
                {
                    this.RotateRight(Parent);
                    Parent = (BSTNode<T>)((Parent.Parent).Parent);
                }
                else
                {
                    Parent = ((BSTNode<T>)Parent.Parent);
                }
            }
        }
        public override bool Add(T data)
        {
            var newNode = this.AddData(data);
            if(newNode == null)
            {
                return false;
            }
            this.ModifyParents((BSTNode<T>)newNode.Parent);
            return true;
        }
        public override T? Find(T data)
        {
            var findNode = FindNode(data);
            if (findNode != null && data.CompareTo(findNode.Data) == 0)
            {
                return findNode.Data;
            }
            return default;
        }
        public bool FindRange(T min, T max, ICollection<T> structure)
        {
            return this.FindNodeRange(this.Root, min, max, structure);            
        }
        private bool FindNodeRange(BSTNode<T> node,T min, T max, ICollection<T> structure)
        {
            BSTNode<T> current = this.Root;
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
                    if(current.Data.CompareTo(min) >= 0)
                    {
                        stack.Push(current);
                        current = current.LeftNode;
                    }
                    else
                    {
                        if(current.RightNode != null)
                        {
                            stack.Push(current.RightNode);
                        }
                        current = null;
                    }
                }                
                while(current == null && stack.Count > 0){
                    current = stack.Pop();
                    if(current.Data.CompareTo(max) <= 0 && current.Data.CompareTo(min) >= 0)
                    {
                        structure.Add(current.Data);
                    }                    
                    current = current.RightNode;
                }
            }

            return true;
        }
        private bool RotateRight(BSTNode<T> node)
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
                        ((BSTNode<T>)leftNode.Parent).LeftNode = leftNode;
                    }
                    else
                    {
                        ((BSTNode<T>)leftNode.Parent).RightNode = leftNode;
                    }
                }
                else
                {
                    leftNode.Parent = null;
                    Root = leftNode;
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
        private bool RotateLeft(BSTNode<T> node)
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
                        ((BSTNode<T>)rightNode.Parent).LeftNode = rightNode;
                    }
                    else
                    {
                        ((BSTNode<T>)rightNode.Parent).RightNode = rightNode;
                    }
                }
                else
                {
                    rightNode.Parent = null;
                    Root = rightNode;
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
        private BSTNode<T>? FindNode(T data)
        {
            var findNode = this.Root;

            while (findNode != null)
            {                
                int compResult = data.CompareTo(findNode.Data);
                if (compResult == 0)
                {
                    return findNode;
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
            return findNode;
        }
        public override bool Delete(T data)
        {
            var removeNode = FindNode(data);
            BSTNode<T> Parent = null;
            //Dont found removed Node
            if (removeNode == null)
            {
                return false;
            }
            //Removed leaf Node
            if (removeNode.IsLeaf())
            {
                return DeleteLeaf(removeNode);
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
                Parent = ((BSTNode<T>)removeNode.Parent);
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
                this.ModifyParents(Parent);
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
                Parent = (BSTNode<T>)removeNode.Parent;
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
                this.ModifyParents(Parent);
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
            Parent = (BSTNode<T>)inorderSuccessor.Parent;
            //Inorder successor is rightNode
            if (removeNode.Data.CompareTo(removeNode.RightNode.Data) == 0)
            {
                if (inorderSuccessor.IsLeaf())
                {
                    removeNode.RightNode = null;
                    Count--;
                    this.ModifyParents(Parent);
                    return true;
                }
                //Has right child
                removeNode.RightNode = inorderSuccessor.RightNode;
                removeNode.RightNode.Parent = removeNode;
                Count--;
                this.ModifyParents(Parent);
                return true;
            }
            //Inorder succesor is leaf
            if (inorderSuccessor.IsLeaf())
            {
                return DeleteLeaf(inorderSuccessor);
            }
            //Inorder succesor has right child
            inorderSuccessor.RightNode.Parent = inorderSuccessor.Parent;
            ((BSTNode<T>)inorderSuccessor.Parent).LeftNode = inorderSuccessor.RightNode;
            Count--;
            this.ModifyParents(Parent);
            return true;
        }
        private bool DeleteLeaf(BSTNode<T> paLeaf)
        {
            //Removed Node is Root
            if (paLeaf.Parent == null)
            {
                Root = null;
                Count--;
                return true;
            }
            //Removed from parent
            var Parent = (BSTNode<T>)paLeaf.Parent;
            var compResult = paLeaf.Data.CompareTo(Parent.Data);
            if (compResult == -1)
            {
                Parent.LeftNode = null;
                Count--;
                this.ModifyParents(Parent);
                return true;
            }
            else if (compResult == 1)
            {
                Parent.RightNode = null;
                Count--;
                this.ModifyParents(Parent);
                return true;
            }
            return false;
        }
        public void Clear()
        {
            this.Root = null;
            this.Count = 0;
        }
        public bool FillWithMedian(List<T> structure)
        {
            if(this.Count > 0)
            {
                return false;
            }
            var stack = new Stack<List<T>>();
            stack.Push(structure);
            while(stack.Count > 0)
            {
                var current = stack.Pop();
                current.Sort();
                if(current.Count  == 1)
                {
                    this.AddData(current[current.Count - 1]);
                }
                else
                {
                    var medianPosition = (current.Count) / 2;                    
                    if(current.Count == 2)
                    {
                        this.AddData(current[0]);
                        this.AddData(current[1]);
                    }
                    else
                    {
                        this.AddData(current[medianPosition]);
                        stack.Push(current.GetRange(0, medianPosition));
                        stack.Push(current.GetRange(medianPosition + 1, (current.Count-1) - (medianPosition) ));
                    }                    
                }                

            }
            return true;
        }
    }
}