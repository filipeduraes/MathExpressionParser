using System;

namespace MathParser
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                string? expression = Console.ReadLine();

                if (expression != null)
                {
                    Parser parser = new(expression);
                    double result = parser.Parse();
                    
                    Console.WriteLine($"Result = {result}");
                }
            }
        }
    }
}