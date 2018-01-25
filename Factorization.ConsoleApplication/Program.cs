using System;
using System.Diagnostics;
using System.Numerics;
using Factorization.Core;

namespace Factorization.ConsoleApplication
{
    public class Program
    {
        public static void Main()
        {
            BigInteger number = BigInteger.Parse(System.Console.ReadLine() ?? throw new InvalidOperationException());

            Stopwatch stopwatch = Stopwatch.StartNew();

            FactorizationResult result = new FactorizationController().ProcessAdvanced(number);

            stopwatch.Stop();

            Console.WriteLine($"P = {result.P}; Q = {result.Q}");
            Console.WriteLine($"{stopwatch.Elapsed}");
        }
    }
}
