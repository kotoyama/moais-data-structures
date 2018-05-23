using System;

namespace DataStructures
{
    public class RandTreeNode<T>
    {
        public RandTreeNode<T> Left { get; set; }
        public RandTreeNode<T> Right { get; set; }
        public T Item { get; set; }
        public int Size { get; set; }

        public RandTreeNode(T item)
        {
            Item = item;
            Size = 1;
        }
    }

    public class RandomizedTree<T> : Tree<T> where T : IComparable
    {
        private RandTreeNode<T> _root;

        private static readonly Random _randomizer = new Random();

        public override bool Contains(T item) => TrySearch(_root, item) != null;

        public override void Insert(T item) => _root = Insert(_root, item);

        public override void Remove(T item)
        {
            if (Contains(item)) _root = Remove(_root, item);
        }

        private static RandTreeNode<T> RotateRight(RandTreeNode<T> node)
        {
            var temp = node.Left;
            if (temp == null || temp.Item.CompareTo(node.Item) == 0) return node;
            node.Left = temp.Right;
            temp.Right = node;
            temp.Size = node.Size;
            FixSize(node);
            FixSize(temp);
            return temp;
        }

        private static RandTreeNode<T> RotateLeft(RandTreeNode<T> node)
        {
            var temp = node.Right;
            if (temp == null || temp.Item.CompareTo(node.Item) == 0) return node;
            node.Right = temp.Left;
            temp.Left = node;
            temp.Size = node.Size;
            FixSize(node);
            FixSize(temp);
            return temp;
        }

        private static RandTreeNode<T> TrySearch(RandTreeNode<T> node, T item)
        {
            while (true)
            {
                if (node == null) return null;
                if (node.Item.CompareTo(item) == 0) return node;
                node = item.CompareTo(node.Item) < 0 ? node.Left : node.Right;
            }
        }

        private static RandTreeNode<T> Insert(RandTreeNode<T> node, T item)
        {
            if (node == null) return new RandTreeNode<T>(item);
            if (_randomizer.Next(node.Size + 1) == 0)
                return InsertRoot(node, item);
            if (item.CompareTo(node.Item) > 0)
                node.Left = Insert(node.Left, item);
            else
                node.Right = Insert(node.Right, item);
            FixSize(node);
            return node;
        }

        private static RandTreeNode<T> InsertRoot(RandTreeNode<T> node, T item)
        {
            if (node == null) return new RandTreeNode<T>(item);
            if (item.CompareTo(node.Item) > 0)
            {
                node.Left = InsertRoot(node.Left, item);
                return RotateRight(node);
            }
            node.Right = InsertRoot(node.Right, item);
            return RotateLeft(node);
        }

        private static RandTreeNode<T> Join(RandTreeNode<T> first, RandTreeNode<T> second)
        {
            if (first == null) return second;
            if (second == null) return first;
            if (_randomizer.Next(first.Size + second.Size) < first.Size)
            {
                first.Right = Join(first.Right, second);
                FixSize(first);
                return first;
            }
            second.Left = Join(first, second.Left);
            FixSize(second);
            return second;
        }

        private static RandTreeNode<T> Remove(RandTreeNode<T> node, T item)
        {
            if (node == null) return null;
            if (node.Item.Equals(item))
                return Join(node.Left, node.Right);
            if (node.Item.CompareTo(item) > 0)
                node.Left = Remove(node.Left, item);
            else
                node.Right = Remove(node.Right, item);
            FixSize(node);
            return node;
        }

        private static int GetSize(RandTreeNode<T> node) => node?.Size ?? 0;

        private static void FixSize(RandTreeNode<T> node)
        {
            if (node != null) node.Size = GetSize(node.Left) + GetSize(node.Right) + 1;
        }
    }
}