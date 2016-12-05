using System;
namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] steps = System.IO.File.ReadAllText(@"..\..\Day01_input.txt").Replace(" ", "").Split(',');

            int x = 0, y = 0;
            if (steps.Length > 0)
            {
                //first face to north
                bool isHorizontalMove = true;
                var isXInverted = false;
                var isYInverted = true;

                foreach (var step in steps)
                {
                    int v = int.Parse(step.Substring(1));
                    if (isHorizontalMove)
                    {
                        int m = !isXInverted != step.StartsWith("R") ? 1 : -1;
                        x += v * m;
                        isYInverted = m > 0;
                    }
                    else
                    {
                        int m = !isYInverted != step.StartsWith("R") ? 1 : -1;
                        y += v * m;
                        isXInverted = m < 0; 
                    }

                    isHorizontalMove = !isHorizontalMove;
                }
            }

            System.Console.WriteLine("pos: {0}, {1}, dist: {2}", x,y, System.Math.Abs(x) + System.Math.Abs(y));
            System.Console.ReadKey();
        }
    }
}
