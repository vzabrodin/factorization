using System;
using System.Diagnostics;
using System.Numerics;
using Factorization.Core;
using Factorization.Core.Interfaces;

namespace Factorization.ConsoleApplication
{
    public class Program
    {
        private static readonly IFactorizationController Controller = new PevnevFactorization();

        public static void Main()
        {
            //BigInteger number = BigInteger.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            //BigInteger number = BigInteger.Parse("916103") * BigInteger.Parse("999217");
            BigInteger number = BigInteger.Parse("111111111111111111111111111111");

            Stopwatch stopwatch = Stopwatch.StartNew();

            FactorizationResult result = Controller.Process(number);

            stopwatch.Stop();

            Console.Beep();

            Console.WriteLine($"P = {result.P}; Q = {result.Q}");
            Console.WriteLine($"{stopwatch.Elapsed}");
        }
    }
}
