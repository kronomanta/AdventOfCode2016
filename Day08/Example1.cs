using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day08
{
    class Example1
    {
        private char[][] _display;
        private Regex rectRegex = new Regex(@"rect (\d+)x(\d+)");
        private Regex rotateRowRegex = new Regex(@"rotate row y=(\d+) by (\d+)");
        private Regex rotateColRegex = new Regex(@"rotate column x=(\d+) by (\d+)");

        public Example1(int width, int height)
        {
            _display = new char[height][];
            for (int i = 0; i < height; i++)
            {
                _display[i] = new char[width];
                for (int j = 0; j < width; j++)
                    _display[i][j] = '.';
            }
        }

        public IEnumerable<string> DoTheMagic()
        {
            foreach (string line in System.IO.File.ReadLines(@"..\..\Day08_input.txt"))
            {
                //reads commands
                Match match = rectRegex.Match(line);
                if (match.Success)
                {
                    Rect(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                    continue;
                }

                match = rotateRowRegex.Match(line);
                if (match.Success)
                {
                    RotateRow(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                    continue;
                }


                match = rotateColRegex.Match(line);
                if (match.Success)
                {
                    RotateColumn(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                }

            }

            return _display.Select(x => new string(x));
        }

        public static int CountPixelLit(IEnumerable<string> display)
        {
            return display.SelectMany(x => x).Count(x => x == '#');
        } 

        private void Rect(int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    _display[i][j] = '#';
                }
            }
        }

        private void RotateRow(int row, int step)
        {
            RotateArrayRight(_display[row], step);
        }

        private void RotateColumn(int col, int step)
        {
            char[] column = _display.Select(r => r[col]).ToArray();
            RotateArrayRight(column, step);


            for (int i = 0; i < column.Length; i++)
                _display[i][col] = column[i];
        }

        private static void RotateArrayRight<T>(T[] array, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");
            if (count == 0)
                return;

            count %= array.Length;
            T[] tmp = new T[count];

            //kieső elemek mennek a tempbe
            Array.Copy(array, array.Length - count, tmp, 0, count);
            //shift
            Array.Copy(array, 0, array, count, array.Length - count);
            //elejére a kiesők
            Array.Copy(tmp, array, count);
        }

    }
}
