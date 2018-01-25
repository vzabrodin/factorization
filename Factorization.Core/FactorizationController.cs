﻿using System.Numerics;
using System.Threading.Tasks;

namespace Factorization.Core
{
    public class FactorizationController
    {
        public FactorizationResult Process(BigInteger n)
        {
            BigInteger p = n.Sqtr();
            BigInteger q = p + 1;
            return Process(n, p, q);
        }

        public FactorizationResult ProcessAdvanced(BigInteger n)
        {
            BigInteger p = n.Sqtr();
            BigInteger q = p + 1;

            Task<FactorizationResult>[] tasks =
            {
                Task.Run(() => Process(n, p, q)),
                Task.Run(() => Process(n, p / 2, q * 2)),
                Task.Run(() => Process(n, p / 3, q * 3)),
                Task.Run(() => Process(n, p / 5, q * 5))
            };

            int taskIndex = Task.WaitAny(tasks);
            return tasks[taskIndex].Result;
        }

        public Task<FactorizationResult> ProcessAsync(BigInteger n) => Task.Run(() => Process(n));

        public Task<FactorizationResult> ProcessAdvancedAsync(BigInteger n) => Task.Run(() => ProcessAdvanced(n));

        private FactorizationResult Process(BigInteger n, BigInteger p, BigInteger q)
        {
            BigInteger delta = n - p * q;

            while (true)
            {
                if (delta == 0)
                    return new FactorizationResult(p, q);

                if (delta > 0)
                {
                    q++;
                    delta -= p; // for optimization
                }

                if (delta < 0)
                {
                    if (BigIntegerExtentions.TryQuadraticEquation(2, q - 2 * p, delta, out var x1, out var x2)
                        && (x1 > 0 || x2 > 0))
                    {
                        BigInteger i = x1 > 0 ? x1 : x2;
                        q += i;
                        p -= i;

                        delta = n - p * q;
                    }
                    else
                    {
                        p--;
                        delta += q; // for optimization
                    }
                }
            }
        }
    }
}
