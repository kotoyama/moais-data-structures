using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class BinaryHeap<T> : IEnumerable<T> where T : IComparable<T>
    {
        private readonly List<T> _heap;
        private int Count => _heap.Count;
        private bool IsEmpty => Count == 0;

        public BinaryHeap() => _heap = new List<T>();

        public BinaryHeap(IEnumerable<T> items)
        {
            _heap = new List<T>();
            foreach (var item in items)
                Insert(item);
        }

        public void Insert(T item)
        {
            _heap.Add(item);
            HeapifyUp(Count - 1);
        }

        public T Extract()
        {
            if (IsEmpty) throw new InvalidOperationException();
            var root = _heap[0];
            _heap[0] = _heap[Count - 1];
            _heap.RemoveAt(Count - 1);
            HeapifyDown();
            return root;
        }

        public T Peek() => IsEmpty ? throw new InvalidOperationException() : _heap[0];

        private void HeapifyUp(int index)
        {
            var parent = GetParent(index);
            while (_heap[index].CompareTo(_heap[parent]) < 0)
            {
                Swap(parent, index);
                index = parent;
                parent = index % 2 == 0 ? (index - 1) / 2 : index / 2;
            }
        }

        private void HeapifyDown()
        {
            var index = 0;
            var smallest = index;
            var left = GetLeftChild(index);
            var right = GetRightChild(index);
            while (index < Count / 2)
            {
                if (left < Count && _heap[left].CompareTo(_heap[index]) < 0)
                    smallest = left;
                if (right < Count && _heap[right].CompareTo(_heap[smallest]) < 0)
                    smallest = right;
                if (smallest == index) return;
                Swap(smallest, index);
                index = smallest;
                index++;
            }
        }

        public void Clear() => _heap.Clear();

        private static int GetParent(int i) => (i - 1) / 2;

        private static int GetLeftChild(int i) => 2 * i + 1;

        private static int GetRightChild(int i) => 2 * i + 2;

        private void Swap(int i, int j)
        {
            var temp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = temp;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _heap)
                yield return item;
        }
    }
}