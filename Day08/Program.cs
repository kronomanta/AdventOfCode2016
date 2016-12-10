namespace Day08
{
    class Program
    {
        static void Main(string[] args)
        {
            var display = new Example1(width: 50, height: 6).DoTheMagic();
            System.Console.WriteLine(string.Join(System.Environment.NewLine, display));
            System.Console.WriteLine(Example1.CountPixelLit(display));

            System.Console.ReadKey();
        }
    }
}
