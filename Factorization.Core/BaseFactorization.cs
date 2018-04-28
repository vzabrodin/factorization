using System;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Factorization.Core.Interfaces;

namespace Factorization.Core
{
    public abstract class BaseFactorization : IFactorizationController
    {
        private readonly int[] coefficients =
            {1, 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67};

        public virtual FactorizationResult Process(BigInteger n, int threadCount = 1)
        {
            if (threadCount <= 0)
                throw new ArgumentException("Number of threads cannot be less or equal than 0");

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Task<FactorizationResult>[] tasks = Enumerable.Range(1, threadCount)
                .Select(i => Task.Run(() => Process(n, i, threadCount, cancellationToken), cancellationToken))
                .ToArray();

            int taskIndex = Task.WaitAny(tasks);
            cancellationTokenSource.Cancel(throwOnFirstException: true);

            return tasks[taskIndex].Result;
        }

        public virtual Task<FactorizationResult> ProcessAsync(BigInteger n, int threadCount = 1)
            => Task.Run(() => Process(n, threadCount));

        protected abstract FactorizationResult Process(BigInteger n, int threadNumber, int threadCount,
            CancellationToken cancellationToken);
    }
}