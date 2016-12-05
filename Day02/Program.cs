using System;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = "";
            int row = 1, col = 1;

            foreach(string code in System.IO.File.ReadLines(@"..\..\Day02_input.txt"))
            {
                foreach (char x in code)
                {
                    switch (x)
                    {
                        case 'U':
                            if (row > 0) row--;
                            break;
                        case 'D':
                            if (row < 2) row++;
                            break;
                        case 'R':
                            if (col < 2) col++;
                            break;
                        case 'L':
                            if (col > 0) col--;
                            break;
                    }
                }

                key += row * 3 + col + 1;
            }

            Console.WriteLine("key: {0}", key);
            Console.ReadKey();
        }


    }
}
