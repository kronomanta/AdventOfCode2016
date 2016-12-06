using System;
using System.Collections.Generic;
using System.Linq;

namespace Day01
{
    class Program
    {
        class Line
        {
            public Tuple<int, int> Start;
            public Tuple<int, int> End;

            private bool IsInLine(Tuple<int,int> point)
            {
                if (Start.Item1 < End.Item1)
                {
                    if (!(Start.Item1 <= point.Item1 && point.Item1 <= Start.Item1))
                        return false;
                }
                else
                {
                    if (!(End.Item1 <= point.Item1 && point.Item1 <= Start.Item1))
                        return false;
                }

                if (Start.Item2 < End.Item2)
                {
                    if (!(Start.Item2 <= point.Item2 && point.Item2 <= Start.Item2))
                        return false;
                }
                else
                {
                    if (!(End.Item2 <= point.Item2 && point.Item2 <= Start.Item2))
                        return false;
                }

                return true;
            }

            public Tuple<int,int> Intersection(Line line)
            {
                //Ax + By = C
                float A1 = End.Item2 - Start.Item2;
                float B1 = Start.Item1 - End.Item1;
                float C1 = A1 * Start.Item1 + B1 * Start.Item2;


                float A2 = line.End.Item2 - line.Start.Item2;
                float B2 = line.Start.Item1 - line.End.Item1;
                float C2 = A2 * line.Start.Item1 + B2 * line.Start.Item2;

                //delta 
                float delta = A1 * B2 - A2 * B1;
                if (delta == 0)
                    return null;

                var intersection = new Tuple<int, int>(
                    (int)((B2 * C1 - B1 * C2) / delta),
                    (int)((A1 * C2 - A2 * C1) / delta)
                );

                if (intersection.Equals(Start) || intersection.Equals(End) ||
                    intersection.Equals(line.Start) || intersection.Equals(line.End) ||
                    !IsInLine(intersection) || !line.IsInLine(intersection))
                    return null;
                return intersection;
            }
        }

        static void Main(string[] args)
        {

            string[] steps = System.IO.File.ReadAllText(@"..\..\Day01_input.txt").Replace(" ", "").Split(',');

            List<Line> positions = new List<Line>();

            int x = 0, y = 0;
            if (steps.Length > 0)
            {
                //first face to north
                bool isHorizontalMove = true;
                var isXInverted = false;
                var isYInverted = true;

                foreach (var step in steps)
                {
                    var line = new Line();
                    line.Start = new Tuple<int, int>(x, y);

                    int v = int.Parse(step.Substring(1));
                    if (isHorizontalMove)
                    {
                        int m = !isXInverted != step.StartsWith("R") ? -1 : 1;
                        x += v * m;
                        isYInverted = m > 0;
                    }
                    else
                    {
                        int m = !isYInverted != step.StartsWith("R") ? -1 : 1;
                        y += v * m;
                        isXInverted = m < 0; 
                    }

                    isHorizontalMove = !isHorizontalMove;

                    //undone
                    line.End = new Tuple<int, int>(x, y);

                    Tuple<int, int> intersection = positions.Select(l => l.Intersection(line)).FirstOrDefault(l => l != null);
                    if (intersection != null)
                    {
                        x = intersection.Item1;
                        y = intersection.Item2;
                        break;
                    }

                    positions.Add(line);
                }
            }

            System.Console.WriteLine("pos: {0}, {1}, dist: {2}", x,y, System.Math.Abs(x) + System.Math.Abs(y));
            System.Console.ReadKey();
        }
    }
}
