using System.Diagnostics;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            var hasher = new Hasher("cuanljph");
            //var hasher = new Hasher("abc");

            int result = -1;
            Stopwatch watcher = new Stopwatch();

            watcher.Start();
            result = hasher.Part01();
            watcher.Stop();

            System.Console.WriteLine("part01: {0}, elapsed: {1}", result, watcher.Elapsed);


            watcher.Start();
            result = hasher.Part02();
            watcher.Stop();

            System.Console.WriteLine("part02: {0}, elapsed: {1}", result, watcher.Elapsed);

            System.Console.ReadKey();
        }
    }
}
