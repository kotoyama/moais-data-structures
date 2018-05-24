using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    public class GraphNode<T>
    {
        public T Item { get; set; }
        private readonly List<GraphNode<T>> _incidentNodes;

        public GraphNode(T item)
        {
            Item = item;
            _incidentNodes = new List<GraphNode<T>>();
        }

        public IEnumerable<GraphNode<T>> GetIncidentNodes()
        {
            foreach (var node in _incidentNodes)
                yield return node;
        }

        public void Connect(GraphNode<T> node)
        {
            _incidentNodes.Add(node);
            node._incidentNodes.Add(this);
        }

        public IEnumerable<GraphNode<T>> BreadthSearch(GraphNode<T> start)
        {
            var visited = new HashSet<GraphNode<T>>();
            var queue = new System.Collections.Generic.Queue<GraphNode<T>>();
            visited.Add(start);
            queue.Enqueue(start);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                yield return node;
                foreach (var next in node._incidentNodes.Where(x => !visited.Contains(x)))
                {
                    visited.Add(next);
                    queue.Enqueue(next);
                }
            }
        }

        public IEnumerable<GraphNode<T>> DepthSearch(GraphNode<T> start)
        {
            var visited = new HashSet<GraphNode<T>>();
            var stack = new System.Collections.Generic.Stack<GraphNode<T>>();
            visited.Add(start);
            stack.Push(start);
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                yield return node;
                foreach (var nextNode in node._incidentNodes.Where(x => !visited.Contains(x)))
                {
                    visited.Add(nextNode);
                    stack.Push(nextNode);
                }
            }
        }

        public List<GraphNode<T>> FindPath(GraphNode<T> start, GraphNode<T> end)
        {
            var path = new Dictionary<GraphNode<T>, GraphNode<T>> { [start] = null };
            var queue = new System.Collections.Generic.Queue<GraphNode<T>>();
            queue.Enqueue(start);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                foreach (var next in node._incidentNodes)
                {
                    if (path.ContainsKey(next)) continue;
                    path[next] = node;
                    queue.Enqueue(next);
                }
                if (path.ContainsKey(end)) break;
            }
            var pathItem = end;
            var result = new List<GraphNode<T>>();
            while (pathItem != null)
            {
                result.Add(pathItem);
                pathItem = path[pathItem];
            }
            result.Reverse();
            return result;
        }

        public bool HasCycle(List<GraphNode<T>> graph)
        {
            var visited = new HashSet<GraphNode<T>>();
            var finished = new HashSet<GraphNode<T>>();
            var stack = new System.Collections.Generic.Stack<GraphNode<T>>();
            visited.Add(graph.First());
            stack.Push(graph.First());
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                foreach (var next in node._incidentNodes.Where(x => !finished.Contains(x)))
                {
                    if (visited.Contains(next)) return true;
                    stack.Push(next);
                    visited.Add(next);
                }
                finished.Add(node);
            }
            return false;
        }
    }

    public class Graph<T>
    {
        private readonly GraphNode<T>[] _nodes;

        public Graph(int count) => _nodes = Enumerable.Range(0, count).Select(z => new GraphNode<T>((T)(object)z)).ToArray();

        public List<List<GraphNode<T>>> FindConnectedComponents(Graph<T> graph)
        {
            var result = new List<List<GraphNode<T>>>();
            var marked = new HashSet<GraphNode<T>>();
            while (true)
            {
                var next = graph._nodes.FirstOrDefault(node => !marked.Contains(node));
                if (next == null) break;
                var breadthSearch = next.BreadthSearch(next).ToList();
                result.Add(breadthSearch.ToList());
                foreach (var node in breadthSearch)
                    marked.Add(node);
            }
            return result;
        }      
    }
}