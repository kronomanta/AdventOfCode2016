using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            //matches 'abba', not matches 'aaaa'
            Regex patternToFind = new Regex(@"(.)((?!\1).)\2\1");
            Regex patternNotToFind = new Regex(@"\[[^\]]*(.)((?!\1).)\2\1[^\[]*\]");

            int sum1 = 0, sum2 = 0;

            foreach (string line in System.IO.File.ReadLines(@"..\..\Day07_input.txt"))
            {
                if (!patternNotToFind.IsMatch(line) && patternToFind.IsMatch(line))
                    sum1++;

                if (SupportSSL(line))
                    sum2++;
            }

            System.Console.WriteLine(sum1);
            System.Console.WriteLine(sum2);

            System.Console.ReadKey();
        }
        
        private static bool SupportSSL(string line)
        {
            var segments = line.Split('[', ']');
            for (int i = 0; i < segments.Length; i += 2)
            {
                foreach (string aba in GetABAs(segments[i]))
                {
                    //megnézem mindegyik illeszkedőre, ami kívül van, hogy tartozik-e hozzá belül is a megfordított
                    for (int j = 1; j < segments.Length; j += 2)
                    {
                        if (segments[j].Contains(aba[1].ToString() + aba[0] + aba[1]))
                            return true;
                    }
                }
            }

            return false;
        }

        static IEnumerable<string> GetABAs(string input)
        {
            for (int i = 0; i < input.Length - 2; i++)
            {
                if (input[i] == input[i + 2] && input[i] != input[i + 1])
                    yield return (input[i].ToString() + input[i + 1] + input[i + 2]);
            }
        }
    }
}
