using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day09
{
    class Program
    {
        static Regex markerReg = new Regex(@"(\d+x\d+)");

        static void Main(string[] args)
        {

            int part1 = 0, part2 = 0;
            foreach (string line in System.IO.File.ReadLines(@"..\..\Day09_input.txt"))
            {
                part1 += DecompressLinePart1(line).Length;
                part2 += DecompressLinePart2(line).Sum(x => x.Length);
            }

            System.Console.WriteLine("part1: {0}", part1);
            System.Console.WriteLine("part2: {0}", part2);
            System.Console.ReadKey();
        }

        private enum State
        {
            NormalLetter,
            CharSeqSelector,
            RepetitionSelector
        }

        public static string DecompressLinePart1(string line)
        {
            return string.Concat(DecompressLine(line));
        }

        public static List<string> DecompressLine(string line)
        {
            if (!markerReg.IsMatch(line)) return new List<string> { line };

            List<string> segments = new List<string>();

            StringBuilder decompressedData = new StringBuilder();
            var state = State.NormalLetter;

            int charSeqSize = 0;

            string numbers = "";

            for(int i = 0; i < line.Length; i++)
            {
                switch (state)
                {
                    case State.NormalLetter:
                        if (line[i] == '(')
                        {
                            if (decompressedData.Length > 0)
                            {
                                segments.Add(decompressedData.ToString());
                                decompressedData.Clear();
                            }
                            
                            state = State.CharSeqSelector;
                        }
                        else
                            decompressedData.Append(line[i]);
                        break;
                    case State.CharSeqSelector:
                        if (line[i] == 'x')
                        {
                            state = State.RepetitionSelector;
                            charSeqSize = int.Parse(numbers);
                            numbers = "";
                        }
                        else
                        {
                            numbers += line[i];
                        }
                        break;
                    case State.RepetitionSelector:
                        if (line[i] == ')')
                        {
                            for(int j=0; j< int.Parse(numbers); j++)
                            {
                                decompressedData.Append(line.Substring(i + 1, charSeqSize));
                            }

                            if (decompressedData.Length > 0)
                            {
                                segments.Add(decompressedData.ToString());
                                decompressedData.Clear();
                            }

                            state = State.NormalLetter;
                            //move forward
                            i += charSeqSize;
                            numbers = "";

                        }
                        else
                        {
                            numbers += line[i];
                        }
                        break;
                }

            }

            //remaining letters
            if (decompressedData.Length > 0)
            {
                segments.Add(decompressedData.ToString());
                decompressedData.Clear();
            }

            return segments;
        }

        public static List<string> DecompressLinePart2(string line)
        {
            //UNDONE 2016.12.11, outofmemoryexception :)
            return new List<string>();



            List<string> final = new List<string>();
            Queue<string> segments = new Queue<string>();
            segments.Enqueue(line);

            do
            {
                string input = segments.Dequeue();
                var segs = DecompressLine(input);
                if (segs[0] == input)
                {
                    //processed
                    final.Add(segs[0]);
                }
                else
                {
                    segs.ForEach(segments.Enqueue);
                }

            }
            while (segments.Count > 0);
            
            return final;
        }

    }
}
