using System;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Factorization.Core
{
    public class FactorizationController
    {
        private readonly int[] coefficients =
            {1, 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67};

        public FactorizationResult ProcessMulticore(BigInteger n, int threadCount)
        {
            if (threadCount <= 0)
                throw new ArgumentException("Number of threads cannot be less or equal than 0");
            if (threadCount > Environment.ProcessorCount)
                throw new ArgumentException("Number of threads cannot be greater than number of processors");

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            BigInteger p = n.Sqtr();
            BigInteger q = p + 1;

            Task<FactorizationResult>[] tasks = coefficients
                .Take(threadCount)
                .Select(i => Task.Run(() => Process(n, p / i, q * i, i), cancellationToken))
                .ToArray();

            int taskIndex = Task.WaitAny(tasks);
            cancellationTokenSource.Cancel();

            return tasks[taskIndex].Result;
        }

        public FactorizationResult ProcessMulticore(BigInteger n)
            => ProcessMulticore(n, Environment.ProcessorCount);

        public Task<FactorizationResult> ProcessMulticoreAsync(BigInteger n)
            => Task.Run(() => ProcessMulticore(n));

        public Task<FactorizationResult> ProcessMulticoreAsync(BigInteger n, int threadCount)
            => Task.Run(() => ProcessMulticore(n, threadCount));

        public FactorizationResult Process(BigInteger n)
        {
            BigInteger p = n.Sqtr();
            BigInteger q = p + 1;

            return Process(n, p, q, 1);
        }

        public Task<FactorizationResult> ProcessAsync(BigInteger n)
            => Task.Run(() => Process(n));

        private FactorizationResult Process(BigInteger n, BigInteger p, BigInteger q, int coefficient)
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
                    if (BigIntegerExtentions.TrySolveQuadraticEquation(coefficient, q - coefficient * p, delta,
                            out BigInteger x1, out BigInteger x2)
                        && (x1 > 0 || x2 > 0))
                    {
                        BigInteger i = x1 > 0 ? x1 : x2;
                        q += i;
                        p -= i * coefficient;

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
