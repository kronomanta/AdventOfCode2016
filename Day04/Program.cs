using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            var checksumRegex = new Regex(@"\[[\w]+\]");
            int realIDSum = 0;
            foreach (string line in System.IO.File.ReadLines(@"..\..\Day04_input.txt"))
            {
                string[] segments = line.Split('-');

                Dictionary<char, int> letters = new Dictionary<char, int>();
                foreach(string seg in segments.Take(segments.Length - 1))
                {
                    foreach(char c in seg)
                    {
                        if (letters.ContainsKey(c))
                        {
                            letters[c]++;
                        }else
                        {
                            letters.Add(c, 1);
                        }
                    }
                }

                string lastSeg = segments[segments.Length - 1];
                var checksumLetters = checksumRegex.Match(lastSeg).Value;
                checksumLetters = checksumLetters.Substring(1, checksumLetters.Length - 2);

                if (string.Join("", letters.OrderByDescending(x => x.Value).ThenBy(x => x.Key).Take(5).Select(x => x.Key)) == checksumLetters)
                    realIDSum += int.Parse(lastSeg.Substring(0, lastSeg.IndexOf('[')));
            }


            System.Console.WriteLine("real sectorID sum: {0}", realIDSum);
            System.Console.ReadKey();
        }
    }
}
