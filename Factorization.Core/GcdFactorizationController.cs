using System.Numerics;

namespace Factorization.Core
{
    public class GcdFactorizationController : FactorizationController
    {
        protected override void GetPAndQ(BigInteger n, out BigInteger p, out BigInteger q)
        {
            p = n.Sqtr();
            q = n - p;
        }

        // ReSharper disable once RedundantAssignment
        protected override FactorizationResult Process(BigInteger n, BigInteger p, BigInteger q, int coefficient)
        {
            p = p * coefficient;
            q = n - p;

            BigInteger d;
            do
            {
                d = BigIntegerExtentions.Gcd(p, q);

                if (d > 1)
                    continue;

                p--;
                q++;
            } while (d == 1);

            return new FactorizationResult(d, n / d);
        }
    }
}
