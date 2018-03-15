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
            BigInteger number = BigInteger.Parse(Console.ReadLine() ?? throw new InvalidOperationException());

            Stopwatch stopwatch = Stopwatch.StartNew();

            FactorizationResult result = new GcdFactorizationController().Process(number, 8);

            stopwatch.Stop();

            System.Media.SystemSounds.Hand.Play();

            Console.WriteLine($"P = {result.P}; Q = {result.Q}");
            Console.WriteLine($"{stopwatch.Elapsed}");
        }
    }
}
