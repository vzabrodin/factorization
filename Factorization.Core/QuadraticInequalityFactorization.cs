using System.Numerics;
using System.Threading;
using Factorization.Core.Extensions;

namespace Factorization.Core
{
    public class QuadraticInequalityFactorization : BaseFactorization
    {
        private readonly int[] coefficients =
            {1, 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67};

        protected override FactorizationResult Process(BigInteger n, int threadNumber, int threadCount,
            CancellationToken cancellationToken)
        {
            BigInteger p, q;
            GetPAndQ(n, out p, out q);
            return Process(n, p, q, coefficients[threadNumber - 1], cancellationToken);
        }

        protected virtual void GetPAndQ(BigInteger n, out BigInteger p, out BigInteger q)
        {
            p = n.Sqtr();
            q = p + 1;
        }

        protected virtual FactorizationResult Process(BigInteger n, BigInteger p, BigInteger q,
            int coefficient, CancellationToken cancellationToken)
        {
            p = p / coefficient;
            q = q * coefficient;

            BigInteger delta = n - p * q;

            while (!cancellationToken.IsCancellationRequested)
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
                    BigInteger x1, x2;
                    if (BigIntegerExtentions.TrySolveQuadraticEquation(coefficient, q - coefficient * p, delta,
                            out x1, out x2)
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

            return FactorizationResult.Failed;
        }
    }
}
