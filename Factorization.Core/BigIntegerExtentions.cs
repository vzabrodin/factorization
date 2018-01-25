using System;
using System.Numerics;

namespace Factorization.Core
{
    public static class BigIntegerExtentions
    {
        public static BigInteger Sqtr(this BigInteger number) => (BigInteger) Math.Exp(BigInteger.Log(number) / 2);

        public static bool TryQuadraticEquation(BigInteger a, BigInteger b, BigInteger c, out BigInteger x1, out BigInteger x2)
        {
            x1 = BigInteger.Zero;
            x2 = BigInteger.Zero;

            BigInteger discriminant = b * b - 4 * 2 * c;
            if (discriminant < 0)
                return false;

            BigInteger discriminantSqrt = discriminant.Sqtr();

            x1 = (-b - discriminantSqrt) / (2 * a);
            x2 = (-b + discriminantSqrt) / (2 * a);

            return true;
        }
    }
}
