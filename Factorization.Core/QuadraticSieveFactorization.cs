using System.Numerics;
using System.Threading;
using Factorization.Core.Extensions;

namespace Factorization.Core
{
    public class QuadraticSieveFactorization : BaseFactorization
    {
        protected override FactorizationResult Process(BigInteger n, int threadNumber, int threadCount,
            CancellationToken cancellationToken)
        {
            for (BigInteger z = n.Sqtr() + threadCount; !cancellationToken.IsCancellationRequested; z += threadNumber)
            {
                BigInteger p = BigInteger.ModPow(z, 2, n);

                if (!p.IsFullSquare())
                    continue;

                BigInteger q = BigIntegerExtentions.GreatestCommonDivisor(z - p.Sqtr(), n);
                if (q > 1)
                    return new FactorizationResult(q, n / q);
            }

            return FactorizationResult.Failed;
        }
    }
}