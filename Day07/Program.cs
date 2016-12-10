using System.Linq;
using System.Text.RegularExpressions;

namespace Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            //matches 'abba', not matches 'aaaa'
            //example 1
            //Regex patternToFind = new Regex(@"(.)((?!\1).)\2\1");
            //Regex patternNotToFind = new Regex(@"\[[^\]]*(.)((?!\1).)\2\1[^\[]*\]");

            //example2
            Regex patternToFind = new Regex(@"\]?[^\[^\]]*(.)((?!\1).)\1[^\[^\]]*\[?");
            string squarePattern = @"\[[^\]]*{0}{1}{0}[^\[]*\]";


            int counter = 0;
            foreach (string line in System.IO.File.ReadLines(@"..\..\Day07_input.txt"))
            {
                MatchCollection matches = patternToFind.Matches(line);
                if (matches.Count == 0) continue;

                bool found = false;
                foreach(Match match in matches)
                {
                    if (!new Regex(string.Format(squarePattern, match.Groups[1], match.Groups[2])).IsMatch(line) &&
                        new Regex(string.Format(squarePattern, match.Groups[2], match.Groups[1])).IsMatch(line))
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                    counter++;
            }

            System.Console.WriteLine(counter);

            System.Console.ReadKey();
        }
    }
}
