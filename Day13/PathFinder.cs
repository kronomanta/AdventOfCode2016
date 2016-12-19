using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    class PathFinder
    {
        public struct Point
        {
            public int X;
            public int Y;

            public override string ToString()
            {
                return "X=" + X + ", Y=" + Y;
            }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public IEnumerable<Point> GetNeighbours()
            {
                for (int a = -1; a <= 1; a += 2)
                {
                    Point n = new Point(X, Y + a);
                    if (n.Y < 0) continue;
                    yield return n;

                    n = new Point(X + a, Y);
                    if (n.X < 0) continue;
                    yield return n;
                }
            }
        }


        readonly int _magicNumber;
        public PathFinder(int magicNumber)
        {
            _magicNumber = magicNumber;
        }

        int HeuristicCostEstimate(Point s, Point f)
        {
            return System.Math.Abs(s.X - f.X) + System.Math.Abs(s.Y - f.Y);
        }

        public List<Point> Path(Point s, Point f)
        {
            //bejárt
            HashSet<Point> closedSet = new HashSet<Point>();

            Queue<Point> openSet = new Queue<Point>();
            openSet.Enqueue(s);

            Dictionary<Point, Point> cameFrom = new Dictionary<Point, Point>();
            //globalscore
            Dictionary<Point, int> gScore = new Dictionary<Point, int>();
            gScore.Add(s, 0);

            //total cost by passing this node
            Dictionary<Point, int> fScore = new Dictionary<Point, int>();
            fScore.Add(s, HeuristicCostEstimate(s, f));

            while (openSet.Count > 0)
            {
                Point current = openSet.Dequeue();
                if (current.Equals(f))
                    return ReconstructPath(cameFrom, current);

                closedSet.Add(current);

                foreach (Point neighbor in current.GetNeighbours())
                {
                    if (closedSet.Contains(neighbor)) continue;
                    if (!IsSpace(neighbor.X, neighbor.Y)) continue;

                    // The distance from start to a neighbor
                    int tentative_gScore = gScore[current] + 1;

                    if (!openSet.Contains(neighbor))
                        openSet.Enqueue(neighbor);
                    else
                    {
                        if (gScore.ContainsKey(neighbor) && tentative_gScore >= gScore[neighbor])
                            continue;       // This is not a better path.
                    }

                    // This path is the best until now. Record it!
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentative_gScore;
                    fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, f);
                }
            }

            return null;
        }

        List<Point> ReconstructPath(Dictionary<Point, Point> cameFrom, Point current)
        {
            List<Point> path = new List<Point>() { current };

            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Add(current);
            }

            path.Reverse();

            return path;
        }

        bool IsSpace(int x, int y)
        {
            int sum = (x * x + 3 * x + 2 * x * y + y + y * y) + _magicNumber;
            return System.Convert.ToString(sum, 2).Count(i => i == '1') % 2 == 0;
        }
    }



}
