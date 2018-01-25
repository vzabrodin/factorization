using System.Numerics;

namespace Factorization.Core
{
    public class FactorizationResult
    {
        public FactorizationResult(BigInteger p, BigInteger q)
        {
            P = p;
            Q = q;
        }

        public BigInteger P { get; set; }

        public BigInteger Q { get; set; }
    }
}
