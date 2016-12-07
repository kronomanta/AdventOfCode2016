using System.Linq;
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

            int counter = 0;
            foreach (string line in System.IO.File.ReadLines(@"..\..\Day07_input.txt").Where(x => !patternNotToFind.IsMatch(x) && patternToFind.IsMatch(x)))
            {
                counter++;
            }

            System.Console.WriteLine(counter);

            System.Console.ReadKey();
        }
    }
}
