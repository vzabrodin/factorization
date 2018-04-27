using System.Numerics;

namespace Factorization.Core
{
    public class FactorizationResult
    {
        private FactorizationResult()
        {
            Success = false;
        }

        public FactorizationResult(BigInteger p, BigInteger q)
        {
            Success = true;
            P = p;
            Q = q;
        }

        public BigInteger P { get; }

        public BigInteger Q { get; }

        public bool Success { get; }

        public static FactorizationResult Failed = new FactorizationResult();
    }
}
