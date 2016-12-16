using System;
using System.Collections.Generic;

namespace Day12
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("part1: {0}", DoTheMagic(0));
            Console.WriteLine("part2: {0}", DoTheMagic(1));
            Console.ReadKey();
        }

        static int DoTheMagic(int defaultC)
        {
            Dictionary<string, int> registers = new Dictionary<string, int> { { "a", 0 }, { "b", 0 }, { "c", defaultC }, { "d", 0 }, };
            string[] commands = System.IO.File.ReadAllLines(@"..\..\Day12_input.txt");

            int pc = 0;
            do
            {
                int step = 1;
                string[] args;
                string command = commands[pc];
                switch (command.Substring(0, 3))
                {
                    case "cpy":
                        args = command.Substring(4).Split(' ');
                        int x;
                        if (int.TryParse(args[0], out x))
                            registers[args[1]] = x;
                        else
                            registers[args[1]] = registers[args[0]];
                        break;
                    case "inc":
                        registers[command.Substring(4, 1)]++;
                        break;
                    case "dec":
                        registers[command.Substring(4, 1)]--;
                        break;
                    case "jnz":
                        args = command.Substring(4).Split(' ');
                        int y;
                        if (!int.TryParse(args[0], out y))
                            y = registers[args[0]];

                        if (y != 0)
                            step = int.Parse(args[1]);
                        break;
                }

                pc += step;
            } while (pc < commands.Length);


            return registers["a"];
        }
    }
}
