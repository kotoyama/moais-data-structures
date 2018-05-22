using System;

namespace DataStructures
{
    internal class AvlNode<T>
    {
        public AvlNode<T> Left { get; set; }
        public AvlNode<T> Right { get; set; }
        public int Height { get; set; }
        public T Item { get; set; }

        public AvlNode(T item)
        {
            Item = item;
            Height = 1;
        }
    }

    public class AvlTree<T> : Tree<T> where T : IComparable
    {
        private AvlNode<T> _root;
        public int Count { get; private set; }

        public override bool Contains(T item)
        {
            while (true)
            {
                if (_root == null) return false;
                if (_root.Item.CompareTo(item) == 0) return true;
                _root = item.CompareTo(_root.Item) < 0 ? _root.Left : _root.Right;
            }
        }

        public override void Insert(T item)
        {
            _root = Insert(_root, item);
            Count++;
        }

        public override void Remove(T item)
        {
            if (!Contains(item)) return;
            _root = Remove(_root, item);
            Count--;
        }

        private static void RotateRight(AvlNode<T> node)
        {
            var temp = node.Left;
            if (temp == null || temp.Item.CompareTo(node.Item) == 0) return;
            node.Left = temp.Right;
            temp.Right = node;
            FixHeight(node);
            FixHeight(temp);
        }

        private static void RotateLeft(AvlNode<T> node)
        {
            var temp = node.Right;
            if (temp == null || temp.Item.CompareTo(node.Item) == 0) return;
            node.Right = temp.Left;
            temp.Left = node;
            FixHeight(node);
            FixHeight(temp);
        }

        private static AvlNode<T> Insert(AvlNode<T> node, T item)
        {
            if (node == null) return new AvlNode<T>(item);
            Insert(item.CompareTo(node.Item) < 0 ? node.Left : node.Right, item);
            return Balance(node);
        }

        private static AvlNode<T> Remove(AvlNode<T> node, T item)
        {
            if (node == null) return null;
            if (item.CompareTo(node.Item) < 0)
                node.Left = Remove(node.Left, item);
            else if (item.CompareTo(node.Item) > 0)
                node.Right = Remove(node.Right, item);
            else
            {
                if (node.Right == null)
                    return node.Left;
                var min = GetMin(node.Right);
                min.Right = RemoveMin(node.Right);
                min.Left = node.Left;
                return Balance(min);
            }
            return Balance(node);
        }

        private static AvlNode<T> Balance(AvlNode<T> node)
        {
            FixHeight(node);
            if (GetBalanceFactor(node) == 2)
            {
                if (GetBalanceFactor(node.Right) < 0)
                    RotateRight(node.Right);
                RotateLeft(node);
            }
            else if (GetBalanceFactor(node) == -2)
            {
                if (GetBalanceFactor(node.Left) < 0)
                    RotateLeft(node.Left);
                RotateRight(node);
            }
            return node;
        }

        private static AvlNode<T> GetMin(AvlNode<T> node)
        {
            if (node == null) return null;
            while (true)
            {
                if (node?.Left == null) return node;
                node = node.Left;
            }
        }

        private static AvlNode<T> RemoveMin(AvlNode<T> node)
        {
            if (node.Left == null) return node.Right;
            node.Left = RemoveMin(node.Left);
            return Balance(node);
        }

        private static int GetBalanceFactor(AvlNode<T> node) => GetHeight(node?.Right) - GetHeight((node?.Left));

        private static int GetHeight(AvlNode<T> node) => node?.Height ?? 0;

        private static void FixHeight(AvlNode<T> node)
        {
            if (node != null) node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }
    }
}