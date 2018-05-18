using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class Stack<T> : IEnumerable<T> where T : IComparable<T>
    {
        private readonly List<T> _stack;
        public int Count => _stack.Count;
        public bool IsEmpty => _stack.Count == 0;

        public Stack() => _stack = new List<T>();

        public Stack(IEnumerable<T> items)
        {
            _stack = new List<T>();
            foreach (var item in items)
                Push(item);
        }

        public void Push(T item) => _stack.Add(item);

        public T Peek() => IsEmpty ? throw new InvalidOperationException() : _stack[Count - 1];

        public T Pop()
        {
            if (IsEmpty) throw new InvalidOperationException();
            var item = _stack[Count - 1];
            _stack.RemoveAt(Count - 1);
            return item;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
                yield return _stack[i];
        }
    }
}