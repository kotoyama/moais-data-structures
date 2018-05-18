using System;

namespace DataStructures
{
    class Program
    {
        static void Main()
        {
            var stack = new Stack<int>(new [] {1, 2, 3, 4, 5});
            var queue = new Queue<int>(new[] { 10, 1, 2, 3, 4, 5 });
            stack.Pop();
            queue.Dequeue();
            queue.Enqueue(12);

            foreach (var item in stack)
                Console.Write(item + " ");
            Console.WriteLine("\n********");
            foreach (var item in queue)
                Console.Write(item + " ");
            Console.ReadLine();
            /*в общем, здесь можно творить разную магию*/
        }
    }
}
