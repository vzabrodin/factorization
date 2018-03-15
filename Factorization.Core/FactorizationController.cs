using System;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Factorization.Core
{
    public class FactorizationController
    {
        protected readonly int[] Coefficients =
            {1, 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67};

        public FactorizationResult Process(BigInteger n, int threadCount = 1)
        {
            if (threadCount <= 0)
                throw new ArgumentException("Number of threads cannot be less or equal than 0");

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            GetPAndQ(n, out BigInteger p, out BigInteger q);

            Task<FactorizationResult>[] tasks = Coefficients
                .Take(threadCount)
                .Select(i => Task.Run(() => Process(n, p, q, i), cancellationToken))
                .ToArray();

            int taskIndex = Task.WaitAny(tasks);
            cancellationTokenSource.Cancel(throwOnFirstException: true);

            return tasks[taskIndex].Result;
        }

        public Task<FactorizationResult> ProcessAsync(BigInteger n, int threadCount = 1)
            => Task.Run(() =>
            {
                try
                {
                    return Process(n, threadCount);
                }
                catch (OperationCanceledException)
                {
                    return null;
                }
            });

        protected virtual void GetPAndQ(BigInteger n, out BigInteger p, out BigInteger q)
        {
            p = n.Sqtr();
            q = p + 1;
        }

        protected virtual FactorizationResult Process(BigInteger n, BigInteger p, BigInteger q, int coefficient)
        {
            p = p / coefficient;
            q = q * coefficient;

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
