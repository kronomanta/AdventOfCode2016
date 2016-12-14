using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace Day09
{
    class Program
    {
        static Regex markerReg = new Regex(@"(\d+x\d+)");

        static void Main(string[] args)
        {

            long part1 = 0;
            BigInteger part2 = 0;
            foreach (string line in System.IO.File.ReadLines(@"..\..\Day09_input.txt"))
            {
                //part1 += DecompressLinePart1(line).Length;
                part2 += DecompressLinePart2(line);
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
            System.DateTime now = System.DateTime.Now;
            System.Console.WriteLine("Part1 started: {0}", now);
            string decompressed = string.Concat(DecompressLine(line));

            System.DateTime finished = System.DateTime.Now;
            System.Console.WriteLine("Part1 finished: {0}, elapsed: {1}", finished, (finished - now).TotalMilliseconds);


            return decompressed;
        }

        public static IEnumerable<string> DecompressLine(string line)
        {
            if (!markerReg.IsMatch(line))
            {
                yield return line;
            }
            else
            {
                StringBuilder decompressedData = new StringBuilder();
                var state = State.NormalLetter;

                int charSeqSize = 0;

                string numbers = "";

                for (int i = 0; i < line.Length; i++)
                {
                    switch (state)
                    {
                        case State.NormalLetter:
                            if (line[i] == '(')
                            {
                                if (decompressedData.Length > 0)
                                {
                                    yield return decompressedData.ToString();
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
                                for (int j = 0; j < int.Parse(numbers); j++)
                                {
                                    decompressedData.Append(line.Substring(i + 1, charSeqSize));
                                }

                                if (decompressedData.Length > 0)
                                {
                                    yield return decompressedData.ToString();
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
                    yield return decompressedData.ToString();
                }
            }
        }

        public static IEnumerable<BigInteger> CountDecompressedLine(string line)
        {
            if (line.Contains('('))
            {
                StringBuilder decompressedData = new StringBuilder();
                var state = State.NormalLetter;

                int charSeqSize = 0;

                string numbers = "";

                for (int i = 0; i < line.Length; i++)
                {
                    switch (state)
                    {
                        case State.NormalLetter:
                            if (line[i] == '(')
                            {
                                if (decompressedData.Length > 0)
                                {
                                    BigInteger l = 0;
                                    foreach (BigInteger r in CountDecompressedLine(decompressedData.ToString()))
                                        l += r;
                                    yield return l;

                                    decompressedData = new StringBuilder();
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
                                int repetition = int.Parse(numbers);
                                for (int j = 0; j < repetition; j++)
                                {
                                    string data = line.Substring(i + 1, charSeqSize);
                                    decompressedData.Append(data);
                                }

                                if (decompressedData.Length > 0)
                                {
                                    BigInteger l = 0;
                                    foreach (BigInteger r in CountDecompressedLine(decompressedData.ToString()))
                                        l += r;
                                    yield return l;

                                    decompressedData = new StringBuilder();
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
                    BigInteger l = 0;
                    foreach (BigInteger r in CountDecompressedLine(decompressedData.ToString()))
                        l += r;
                    yield return l;
                }
            }
            else
            {
                yield return line.Length;
            }
        }


        public static BigInteger DecompressLinePart2(string line)
        {
            System.DateTime now = System.DateTime.Now;
            System.Console.WriteLine("Part2 started: {0}", now);
            BigInteger length = 0;
            foreach (BigInteger l in CountDecompressedLine(line))
                length += l;

            System.DateTime finished = System.DateTime.Now;
            System.Console.WriteLine("Part2 finished: {0}, elapsed: {1}", finished, finished - now);

            return length;
        }

    }
}
