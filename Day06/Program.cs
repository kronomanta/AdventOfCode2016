using System.Linq;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] columns = null;
            foreach (string line in System.IO.File.ReadLines(@"..\..\Day06_input.txt"))
            {
                if (columns == null)
                    columns = new string[line.Length];

                for (int i = 0; i < line.Length; i++)
                    columns[i] += line[i];
            }

            string decoded = "";
            foreach (var column in columns)
            {
                decoded += column.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).FirstOrDefault();
            }
                        
            System.Console.WriteLine(decoded);

            decoded = "";
            foreach (var column in columns)
            {
                decoded += column.GroupBy(x => x).OrderBy(x => x.Count()).Select(x => x.Key).FirstOrDefault();
            }

            System.Console.WriteLine(decoded);

            System.Console.ReadKey();

        }
    }
}
