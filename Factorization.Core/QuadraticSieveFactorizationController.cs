using System.Numerics;
using System.Threading;

namespace Factorization.Core
{
    public class QuadraticSieveFactorizationController : BaseFactorizationController
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