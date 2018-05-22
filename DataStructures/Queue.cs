using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class QueueNode<T>
    {
        public T Item { get; set; }
        public QueueNode<T> Next { get; set; }
    }

    public class Queue<T> : IEnumerable<T> where T : IComparable<T>
    {
        public QueueNode<T> Head { get; private set; }
        public QueueNode<T> Tail { get; private set; }
        public bool IsEmpty => Head == null;

        public Queue(IEnumerable<T> items)
        {
            foreach (var item in items)
                Enqueue(item);
        }

        public void Enqueue(T item)
        {
            if (IsEmpty)
                Tail = Head = new QueueNode<T> { Item = item, Next = null };
            else
            {
                var value = new QueueNode<T> { Item = item, Next = null };
                Tail.Next = value;
                Tail = value;
            }
        }

        public T Dequeue()
        {
            if (IsEmpty) throw new InvalidOperationException();
            var item = Head.Item;
            Head = Head.Next;
            if (IsEmpty) Tail = null;
            return item;
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator()
        {
            while (!IsEmpty)
            {
                yield return Head.Item;
                Head = Head.Next;
            }
        }
    }
}