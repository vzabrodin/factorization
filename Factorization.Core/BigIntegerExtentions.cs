using System;
using System.Numerics;

namespace Factorization.Core
{
    public static class BigIntegerExtentions
    {
        public static BigInteger Sqtr(this BigInteger number) => (BigInteger) Math.Exp(BigInteger.Log(number) / 2);

        public static bool TrySolveQuadraticEquation(BigInteger a, BigInteger b, BigInteger c,
            out BigInteger x1, out BigInteger x2)
        {
            x1 = BigInteger.Zero;
            x2 = BigInteger.Zero;

            BigInteger discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
                return false;

            BigInteger discriminantSqrt = discriminant.Sqtr();

            x1 = (-b - discriminantSqrt) / (2 * a);
            x2 = (-b + discriminantSqrt) / (2 * a);

            return true;
        }

        public static BigInteger GreatestCommonDivisor(BigInteger a, BigInteger b)
        {
            return a > b ? GcdInternal(a, b) : GcdInternal(b, a);
        }

        public static void Exchange(ref BigInteger a, ref BigInteger b)
        {
            a = a + b;
            b = a - b;
            a = a - b;
        }

        public static bool IsFullSquare(this BigInteger number)
        {
            BigInteger sqrt = number.Sqtr();
            return number == sqrt * sqrt;
        }

        private static BigInteger GcdInternal(BigInteger a, BigInteger b)
        {
            while (true)
            {
                while (a > b)
                    a = a - b;

                if (a == b)
                    return a;

                Exchange(ref a, ref b);
            }
        }
    }
}
