namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            int possibleCount = 0;
            int rowCounter = 0;
            string[] lines = new string[3];

            foreach(string line in System.IO.File.ReadLines(@"..\..\Day03_input.txt"))
            {
                rowCounter = (++rowCounter % 3);
                lines[rowCounter] = line;

                if (rowCounter == 0)
                {
                    for(int i = 0; i < 3; i++)
                    {
                        int x = int.Parse(lines[0].Substring(5*i, 5));
                        int y = int.Parse(lines[1].Substring(5*i, 5));
                        int z = int.Parse(lines[2].Substring(5*i, 5));

                        if (x + y > z && x + z > y && y + z > x)
                            possibleCount++;
                    }
                    
                }

            }


            System.Console.WriteLine("possible count: {0}", possibleCount);
            System.Console.ReadKey();
        }
    }
}
