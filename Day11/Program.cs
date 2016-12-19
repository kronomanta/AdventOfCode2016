using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day11
{
    class Program
    {
        static List<ItemBase[]> floors = new List<ItemBase[]>();

        static void Main()
        {
            InitFloors();

            ShowFloors();

            Console.ReadKey();
        }

        static void ShowFloors()
        {
            Console.Clear();

            for (int i = 0; i < floors.Count; i++)
            {
                Console.WriteLine("F{0} {1}", i + 1, string.Join(" ", floors[i].Select(f => f.GetName())));
            }

            Console.ReadKey();
        }

        static void InitFloors()
        {

            Regex reg = new Regex(@"((\w+) generator|(\w+)-compatible microchip)");

            List<List<ItemBase>> tempFloors = new List<List<ItemBase>>();
            foreach (string line in System.IO.File.ReadLines(@"..\..\Day11_input.txt"))
            {
                List<ItemBase> floor = new List<ItemBase>();

                //init
                MatchCollection matches = reg.Matches(line);
                foreach (Match match in matches)
                {
                    Item item;
                    if (!string.IsNullOrWhiteSpace(match.Groups[2].Value))
                    {
                        //generator
                        item = new Item(match.Groups[2].Value, ItemType.Generator);
                    }
                    else
                    {
                        //chip
                        item = new Item(match.Groups[3].Value, ItemType.Chip);
                    }

                    floor.Add(item);
                }

                tempFloors.Add(floor);
            }

            int elementCounts = tempFloors.Select(f => f.Count).Sum();
            int shift = 1;
            for (int i = 0; i < tempFloors.Count; i++)
            {
                ItemBase[] floor = new ItemBase[elementCounts + 1];
                for (int j = 0; j < shift; j++)
                {
                    floor[j] = new Dummy();
                }

                for (int j = 0; j < tempFloors[i].Count; j++)
                {
                    floor[shift + j] = tempFloors[i][j];
                }

                for (int j = shift + tempFloors[i].Count; j < elementCounts + 1; j++)
                {
                    floor[j] = new Dummy();
                }

                shift += tempFloors[i].Count;
                floors.Add(floor);
            }

            floors[0][0] = new Item("E", ItemType.Elevator);
        }

    }
}
