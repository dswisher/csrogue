using System;
using System.Collections.Generic;

namespace csrogue
{
    // Note: this is based on an MSDN blog post:
    //   https://blogs.msdn.microsoft.com/ericlippert/2007/10/10/path-finding-using-a-in-c-3-0-part-four/
    public static class AStar
    {
        public static Path<Node> FindPath<Node>(
            Node start,
            Node destination,
            Func<Node, Node, double> distance,
            Func<Node, double> estimate,
            Func<Node, IEnumerable<Node>> neighbors)
        {
            var closed = new HashSet<Node>();
            var queue = new PriorityQueue<double, Path<Node>>();
            queue.Enqueue(0, new Path<Node>(start));
            while (!queue.IsEmpty)
            {
                var path = queue.Dequeue();
                if (closed.Contains(path.LastStep))
                {
                    continue;
                }

                if (path.LastStep.Equals(destination))
                {
                    return path;
                }

                closed.Add(path.LastStep);

                foreach (Node n in neighbors(path.LastStep))
                {
                    double d = distance(path.LastStep, n);
                    var newPath = path.AddStep(n, d);
                    queue.Enqueue(newPath.TotalCost + estimate(n), newPath);
                }
            }

            return null;
        }
    }
}
