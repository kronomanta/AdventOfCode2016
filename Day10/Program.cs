using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day10
{
    class Program
    {
        static List<Node> nodes = new List<Node>();
        static List<KeyValuePair<string, int>> commands = new List<KeyValuePair<string, int>>();

        static void Main(string[] args)
        {
            Regex botReg = new Regex(@"(bot \d+|output \d+)");
            Regex commandReg = new Regex(@"value (\d+) .* (bot \d+|output \d+)");

            
            foreach (string line in System.IO.File.ReadLines(@"..\..\Day10_input.txt"))
            {
                MatchCollection matches = botReg.Matches(line);
                if (matches.Count == 3)
                {
                    Node node = GetNodeByID(matches[0].Groups[1].Value);
                    node.Lower = GetNodeByID(matches[1].Groups[1].Value);
                    node.Higher = GetNodeByID(matches[2].Groups[1].Value);
                    continue;
                }

                Match match = commandReg.Match(line);
                if (match.Success)
                {
                    commands.Add(new KeyValuePair<string, int>(match.Groups[2].Value, Convert.ToInt32(match.Groups[1].Value)));
                }
            }

            commands.ForEach(c => nodes.First(n => n.Id == c.Key).AddChip(c.Value));

            var outputs = nodes.Where(n => n is OutputBin && (n.Id == "output 0" || n.Id == "output 1" || n.Id == "output 2")).ToArray();

            Console.WriteLine("part1: {0}", nodes.Where(n => n.GetChips().Contains(61) && n.GetChips().Contains(17)).Select(n => n.Id).FirstOrDefault());
            Console.WriteLine("part2: {0}", outputs[0].GetChips()[0] * outputs[1].GetChips()[0] * outputs[2].GetChips()[0] );
            Console.ReadKey();
        }

        static Node GetNodeByID(string id)
        {
            Node node = nodes.FirstOrDefault(x => x.Id == id);
            if (node == null)
            {
                if (id.StartsWith("bot"))
                    node = new Bot(id);
                else
                    node = new OutputBin(id);
                nodes.Add(node);
            }

            return node;
        }
    }
}
