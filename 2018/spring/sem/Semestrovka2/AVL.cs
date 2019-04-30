using System;
using System.Collections.Generic;
using System.Text;

namespace Semestrovka2
{
    public class AVL<T>
    {
        private class Node<T>
        {
            public T Value
            {
                get; set;
            }
            public int Key
            {
                get; set;
            }
            public Node<T> Left
            {
                get; set;
            }
            public Node<T> Right
            {
                get; set;
            }
            public Node(int key, T value)
            {
                Key = key;
                Value = value;
            }
        }

        private Node<T> root;

        public void Add(int key, T value)
        {
            Node<T> newItem = new Node<T>(key, value);
            if (root == null)
            {
                root = newItem;
            }
            else
            {
                root = RecursiveInsert(root, newItem);
            }
        }

        private Node<T> RecursiveInsert(Node<T> node, Node<T> n)
        {
            if (node == null)
            {
                node = n;
                return node;
            }
            else if (n.Key < node.Key)
            {
                node.Left = RecursiveInsert(node.Left, n);
                node = BalanceNode(node);
            }
            else if (n.Key > node.Key)
            {
                node.Right = RecursiveInsert(node.Right, n);
                node = BalanceNode(node);
            }
            return node;
        }

        private Node<T> BalanceNode(Node<T> node)
        {
            int b_factor = BalanceFactor(node);
            if (b_factor > 1)
            {
                if (BalanceFactor(node.Left) > 0)
                {
                    node = RotateLL(node);
                }
                else
                {
                    node = RotateLR(node);
                }
            }
            else if (b_factor < -1)
            {
                if (BalanceFactor(node.Right) > 0)
                {
                    node = RotateRL(node);
                }
                else
                {
                    node = RotateRR(node);
                }
            }
            return node;
        }

        public void Delete(int key)
        {
            root = Delete(root, key);
        }

        private Node<T> Delete(Node<T> current, int key)
        {
            Node<T> parent;
            if (current == null)
            {
                return null;
            }
            else
            {
                //Если в левом поддереве
                if (key < current.Key)
                {
                    current.Left = Delete(current.Left, key);
                    if (BalanceFactor(current) == -2)//Балансим(при выходе)
                    {
                        if (BalanceFactor(current.Right) <= 0)
                        {
                            current = RotateRR(current);
                        }
                        else
                        {
                            current = RotateRL(current);
                        }
                    }
                }
                //Если в правом
                else if (key > current.Key)
                {
                    current.Right = Delete(current.Right, key);
                    if (BalanceFactor(current) == 2)//Балансим(при выходе)
                    {
                        if (BalanceFactor(current.Left) >= 0)
                        {
                            current = RotateLL(current);
                        }
                        else
                        {
                            current = RotateLR(current);
                        }
                    }
                }
                //Если нашли
                else
                {
                    if (current.Right != null)
                    {
                        parent = current.Right;
                        while (parent.Left != null)
                        {
                            parent = parent.Left;
                        }
                        current.Key = parent.Key;
                        current.Right = Delete(current.Right, parent.Key);
                        if (BalanceFactor(current) == 2)//Балансим
                        {
                            if (BalanceFactor(current.Left) >= 0)
                            {
                                current = RotateLL(current);
                            }
                            else { current = RotateLR(current); }
                        }
                    }
                    else
                    {   //if current.left != null
                        return current.Left;
                    }
                }
            }
            return current;
        }

        public T Find(int key)
        {
            Node<T> ans = Find(key, root);
            if (ans.Key == key)
            {
                return ans.Value;
            }
            else
            {
                return default(T);
            }
        }

        private Node<T> Find(int key, Node<T> current)
        {

            if (key < current.Key)
            {
                if (key == current.Key)
                {
                    return current;
                }
                else
                {
                    return Find(key, current.Left);
                }
            }
            else
            {
                if (key == current.Key)
                {
                    return current;
                }
                else
                {
                    return Find(key, current.Right);
                }
            }
        }

        private int GetHeight(Node<T> node)
        {
            int height = 0;
            if (node != null)
            {
                int left = GetHeight(node.Left);
                int right = GetHeight(node.Right);
                int max = Math.Max(left, right);
                height = max + 1;
            }
            return height;
        }

        private int BalanceFactor(Node<T> node)
        {
            int left = GetHeight(node.Left);
            int right = GetHeight(node.Right);
            int factor = left - right;
            return factor;
        }

        private Node<T> RotateRR(Node<T> parent)
        {
            Node<T> pivot = parent.Right;
            parent.Right = pivot.Left;
            pivot.Left = parent;
            return pivot;
        }

        private Node<T> RotateLL(Node<T> parent)
        {
            Node<T> pivot = parent.Left;
            parent.Left = pivot.Right;
            pivot.Right = parent;
            return pivot;
        }

        private Node<T> RotateLR(Node<T> parent)
        {
            Node<T> pivot = parent.Left;
            parent.Left = RotateRR(pivot);
            return RotateLL(parent);
        }

        private Node<T> RotateRL(Node<T> parent)
        {
            Node<T> pivot = parent.Right;
            parent.Right = RotateLL(pivot);
            return RotateRR(parent);
        }
    }
}
