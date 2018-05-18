using System;

namespace DataStructures
{
    class Program
    {
        static void Main()
        {
            var stack = new Stack<int>(new [] {1, 2, 3, 4, 5});
            Console.WriteLine(stack.Peek());
            stack.Pop();

            foreach (var item in stack)
                Console.Write(item + " ");
            Console.ReadLine();
        }
    }
}
