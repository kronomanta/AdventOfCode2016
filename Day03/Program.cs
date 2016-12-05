namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            int possibleCount = 0;
            foreach(string line in System.IO.File.ReadLines(@"..\..\Day03_input.txt"))
            {
                int x = int.Parse(line.Substring(0, 5));
                int y = int.Parse(line.Substring(5, 5));
                int z = int.Parse(line.Substring(10, 5));

                if (x + y > z && x + z > y && y + z > x)
                    possibleCount++;
            }


            System.Console.WriteLine("possible count: {0}", possibleCount);
            System.Console.ReadKey();
        }
    }
}
