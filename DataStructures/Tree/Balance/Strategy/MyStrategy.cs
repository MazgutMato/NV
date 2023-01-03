using DataStructures.Tree.Binary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Tree.Balance.Strategy
{
    public class MyStrategy<T> : BalancingStrategy<T> where T : IComparable<T>
    {
        private BSTNode<T>? FindNode(T data, BSTree<T> tree)
        {
            var findNode = tree.Root;

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
        private bool DeleteLeaf(BSTNode<T> paLeaf, BSTree<T> tree)
        {
            //Removed Node is Root
            if (paLeaf.Parent == null)
            {
                tree.Root = null;
                tree.Count--;
                return true;
            }
            //Removed from parent
            var Parent = paLeaf.Parent;
            var compResult = paLeaf.Data.CompareTo(Parent.Data);
            if (compResult == -1)
            {
                Parent.LeftNode = null;
                tree.Count--;
                ModifyParents(Parent, tree);
                return true;
            }
            else if (compResult == 1)
            {
                Parent.RightNode = null;
                tree.Count--;
                ModifyParents(Parent, tree);
                return true;
            }
            return false;
        }
        private void ModifyParents(BSTNode<T> node, BSTree<T> tree)
        {
            if (node == null)
            {
                return;
            }
            var Parent = node.Parent;
            //Random rotate if not balance
            while (Parent != null)
            {
                if (Parent.LeftNode != null && Parent.RightNode != null)
                {
                    Random random = new Random();
                    if (random.Next(0, 1) == 0)
                    {
                        RotateLeft(Parent, tree);
                    }
                    else
                    {
                        RotateRight(Parent, tree);
                    }
                    Parent = Parent.Parent.Parent;
                }
                else if (Parent.LeftNode == null)
                {
                    RotateLeft(Parent, tree);
                    Parent = Parent.Parent.Parent;
                }
                else if (Parent.RightNode == null)
                {
                    RotateRight(Parent, tree);
                    Parent = Parent.Parent.Parent;
                }
                else
                {
                    Parent = Parent.Parent;
                }
            }
        }
        private BSTNode<T> AddData(T data, BSTree<T> tree)
        {
            BSTNode<T>? Parent = null;
            var Child = tree.Root;

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
                tree.Root = newNode;
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
            tree.Count++;
            return newNode;
        }
        public override bool Add(T data, BSTree<T> tree)
        {
            var newNode = AddData(data, tree);
            if (newNode == null)
            {
                return false;
            }
            ModifyParents(newNode.Parent, tree);
            return true;
        }
        public override bool Delete(T data, BSTree<T> tree)
        {
            var removeNode = FindNode(data, tree);
            BSTNode<T> Parent = null;
            //Dont found removed Node
            if (removeNode == null)
            {
                return false;
            }
            //Removed leaf Node
            if (removeNode.IsLeaf())
            {
                return DeleteLeaf(removeNode, tree);
            }
            //Removed node has one child
            if (removeNode.LeftNode == null)
            {
                //Removed Node is Root
                if (removeNode.Parent == null)
                {
                    tree.Root = removeNode.RightNode;
                    tree.Root.Parent = null;
                    tree.Count--;
                    return true;
                }
                //Is right or left son of parent
                Parent = removeNode.Parent;
                var compResult = removeNode.Data.CompareTo(removeNode.Parent.Data);
                if (compResult == -1)
                {
                    removeNode.Parent.LeftNode = removeNode.RightNode;
                    removeNode.RightNode.Parent = removeNode.Parent;
                }
                else if (compResult == 1)
                {
                    removeNode.Parent.RightNode = removeNode.RightNode;
                    removeNode.RightNode.Parent = removeNode.Parent;
                }
                tree.Count--;
                ModifyParents(Parent, tree);
                return true;
            }
            if (removeNode.RightNode == null)
            {
                if (removeNode.Parent == null)
                {
                    tree.Root = removeNode.LeftNode;
                    tree.Root.Parent = null;
                    tree.Count--;
                    return true;
                }
                //Is right or left son of parent
                Parent = removeNode.Parent;
                var compResult = removeNode.Data.CompareTo(removeNode.Parent.Data);
                if (compResult == -1)
                {
                    removeNode.Parent.LeftNode = removeNode.LeftNode;
                    removeNode.LeftNode.Parent = removeNode.Parent;
                }
                else if (compResult == 1)
                {
                    removeNode.Parent.RightNode = removeNode.LeftNode;
                    removeNode.LeftNode.Parent = removeNode.Parent;
                }
                tree.Count--;
                ModifyParents(Parent, tree);
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
            Parent = inorderSuccessor.Parent;
            //Inorder successor is rightNode
            if (removeNode.Data.CompareTo(removeNode.RightNode.Data) == 0)
            {
                if (inorderSuccessor.IsLeaf())
                {
                    removeNode.RightNode = null;
                    tree.Count--;
                    ModifyParents(Parent, tree);
                    return true;
                }
                //Has right child
                removeNode.RightNode = inorderSuccessor.RightNode;
                removeNode.RightNode.Parent = removeNode;
                tree.Count--;
                ModifyParents(Parent, tree);
                return true;
            }
            //Inorder succesor is leaf
            if (inorderSuccessor.IsLeaf())
            {
                return DeleteLeaf(inorderSuccessor, tree);
            }
            //Inorder succesor has right child
            inorderSuccessor.RightNode.Parent = inorderSuccessor.Parent;
            inorderSuccessor.Parent.LeftNode = inorderSuccessor.RightNode;
            tree.Count--;
            ModifyParents(Parent, tree);
            return true;
        }
    }
}
