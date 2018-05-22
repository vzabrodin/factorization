using System;
using System.Diagnostics;
using System.Numerics;
using Factorization.Core;
using Factorization.Core.Interfaces;

namespace Factorization.ConsoleApplication
{
    public class Program
    {
        private static readonly IFactorizationController Controller = new QuadraticInequalityFactorization();

        public static void Main()
        {
            BigInteger number = BigInteger.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            Stopwatch stopwatch = Stopwatch.StartNew();

            FactorizationResult result = Controller.Process(number, Environment.ProcessorCount);

            stopwatch.Stop();

            Console.Beep();

            Console.WriteLine($"P = {result.P}; Q = {result.Q}");
            Console.WriteLine($"{stopwatch.Elapsed}");
        }
    }
}
