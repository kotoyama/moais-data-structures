using System;

namespace DataStructures
{
    public abstract class Tree<T> where T : IComparable
    {
        public abstract bool Contains(T item);
        public abstract void Insert(T item);
        public abstract void Remove(T item);
    }
}