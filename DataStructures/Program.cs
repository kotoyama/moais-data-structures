using System;

namespace DataStructures
{
    class Program
    {
        static void Main()
        {
            var stack = new Stack<int>(new [] {1, 2, 3, 4, 5});
            var queue = new Queue<int>(new [] { 10, 1, 2, 3, 4, 5 });
            var heap = new BinaryHeap<int>(new [] { -1, 12, 10, -10 });
            stack.Pop();
            stack.Push(14);
            queue.Dequeue();
            queue.Enqueue(12);
            heap.Extract();
            heap.Extract();
            heap.Insert(8);

            Console.Write("\nStack: ");
            foreach (var item in stack)
                Console.Write(item + " ");
            Console.Write("\nQueue: ");
            foreach (var item in queue)
                Console.Write(item + " ");
            Console.Write("\nBinary Heap: ");
            foreach (var item in heap)
                Console.Write(item + " ");
            Console.ReadLine();
        }
    }
}