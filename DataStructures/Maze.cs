using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataStructures
{
    public class MazeNode
    {
        public Point Position { get; set; }
        public MazeNode CameFrom { get; set; }
        public int BombCount { get; set; }
        public int PathLengthFromStart { get; set; }
        public int PathLengthEstimation { get; set; }
        public int FullEstimation => PathLengthFromStart + PathLengthEstimation;

        public MazeNode() { }

        public MazeNode(Point point, MazeNode from, int bombCount)
        {
            Position = point;
            CameFrom = from;
            BombCount = bombCount;
        }
    }

    public class Maze
    {
        public static List<Point> FindPath(int[,] field, Point start, Point goal, int bombCount)
        {
            var closed = new Collection<MazeNode>();
            var opened = new Collection<MazeNode>();

            var startNode = new MazeNode()
            {
                Position = start,
                CameFrom = null,
                BombCount = bombCount,
                PathLengthFromStart = 0,
                PathLengthEstimation = GetPathLength(start, goal)
            };
            opened.Add(startNode);
            while (opened.Count > 0)
            {
                var current = opened.OrderBy(node => node.FullEstimation).First();
                if (current.Position == goal) return GetPathForNode(current);
                opened.Remove(current);
                closed.Add(current);
                foreach (var neighbour in GetNeighbours(current, goal, field))
                {
                    if (closed.Count(node => node.Position == neighbour.Position) > 0) continue;
                    var openNode = opened.FirstOrDefault(node => node.Position == neighbour.Position);
                    if (openNode == null) opened.Add(neighbour);
                    else if (openNode.PathLengthFromStart > neighbour.PathLengthFromStart || openNode.BombCount > 0)
                    {
                        openNode.CameFrom = current;
                        openNode.BombCount = bombCount - 1;
                        openNode.PathLengthFromStart = neighbour.PathLengthFromStart;
                    }
                }
            }
            return null;
        }

        private static IEnumerable<MazeNode> GetNeighbours(MazeNode path, Point goal, int[,] field)
        {
            var result = new Collection<MazeNode>();
            var neighbours = new Point[4];

            neighbours[0] = new Point(path.Position.X + 1, path.Position.Y);
            neighbours[1] = new Point(path.Position.X - 1, path.Position.Y);
            neighbours[2] = new Point(path.Position.X, path.Position.Y + 1);
            neighbours[3] = new Point(path.Position.X, path.Position.Y - 1);

            foreach (var point in neighbours)
            {
                if (point.X < 0 || point.X >= field.GetLength(0)) continue;
                if (point.Y < 0 || point.Y >= field.GetLength(1)) continue;
                if ((field[point.X, point.Y] != 0) && (field[point.X, point.Y] != 1)) continue;

                var neighbour = new MazeNode()
                {
                    Position = point,
                    CameFrom = path,
                    PathLengthFromStart = path.PathLengthFromStart + GetDistanceBetweenNeighbours(),
                    PathLengthEstimation = GetPathLength(point, goal)
                };
                result.Add(neighbour);
            }
            return result;
        }

        private static int GetPathLength(Point from, Point to) => Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);

        private static int GetDistanceBetweenNeighbours() => 1;

        private static List<Point> GetPathForNode(MazeNode path)
        {
            var result = new List<Point>();
            var current = path;
            while (current != null)
            {
                result.Add(current.Position);
                current = current.CameFrom;
            }
            result.Reverse();
            return result;
        }
    }
}