using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Factorization.Core
{
    public class FactorizationController
    {
        public FactorizationResult Process(BigInteger n)
        {
            BigInteger p = n.Sqtr();
            BigInteger q = p + 1;
            return Process(n, p, q, 1);
        }

        public FactorizationResult ProcessMulticore(BigInteger n)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            BigInteger p = n.Sqtr();
            BigInteger q = p + 1;

            Task<FactorizationResult>[] tasks =
            {
                Task.Run(() => Process(n, p, q, 1), cancellationToken),
                Task.Run(() => Process(n, p / 2, q * 2, 2), cancellationToken),
                Task.Run(() => Process(n, p / 3, q * 3, 3), cancellationToken),
                Task.Run(() => Process(n, p / 5, q * 5, 5), cancellationToken)
            };

            int taskIndex = Task.WaitAny(tasks);
            cancellationTokenSource.Cancel();

            return tasks[taskIndex].Result;
        }

        public Task<FactorizationResult> ProcessAsync(BigInteger n) => Task.Run(() => Process(n));

        public Task<FactorizationResult> ProcessMulticoreAsync(BigInteger n) => Task.Run(() => ProcessMulticore(n));

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
