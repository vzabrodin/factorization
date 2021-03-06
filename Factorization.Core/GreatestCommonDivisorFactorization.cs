﻿using System.Numerics;
using System.Threading;
using Factorization.Core.Extensions;

namespace Factorization.Core
{
    public class GreatestCommonDivisorFactorization : QuadraticInequalityFactorization
    {
        protected override void GetPAndQ(BigInteger n, out BigInteger p, out BigInteger q)
        {
            p = n.Sqtr();
            q = n - p;
        }

        protected override FactorizationResult Process(BigInteger n, BigInteger p, BigInteger q, int coefficient,
            CancellationToken cancellationToken)
        {
            p = p * coefficient;
            q = n - p;

            BigInteger d;
            do
            {
                d = BigIntegerExtentions.GreatestCommonDivisor(p, q);

                if (d > 1)
                    continue;

                p--;
                q++;
            } while (d == 1 && !cancellationToken.IsCancellationRequested);

            return cancellationToken.IsCancellationRequested
                ? FactorizationResult.Failed
                : new FactorizationResult(d, n / d);
        }
    }
}
