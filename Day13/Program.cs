using System;
using System.Collections.Generic;
using System.Linq;
using static Day13.PathFinder;

namespace Day13
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            const int magicNumber = 1362;

            //test
            //int tx = 7, ty = 4;
            //const int magicNumber = 10;

            var pathFinder = new PathFinder(magicNumber);
            List <Point> path = pathFinder.Path(new Point(1, 1), new Point(31, 39));
            Console.WriteLine("steps:  {0}", path.Count - 1);

            HashSet<Point> inrange = new HashSet<Point>();
            for(int x = 0; x < 50; x++)
            {
                //max 50 steps range
                for (int y = 0; y < 50-x; y++)
                {
                    path = pathFinder.Path(new Point(1, 1), new Point(x,y));
                    if (path == null) continue;
                    foreach (Point p in path.Take(51))
                    {
                        if (p.X == 31 && p.Y== 39)
                        {

                        }
                        inrange.Add(p);
                    }
                }

            }

            Console.WriteLine("in 50 steps:  {0}", inrange.Count);

            Console.ReadKey();
        }

       
    }
}
