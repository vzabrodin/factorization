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
        private CancellationTokenSource cancellationTokenSource;

        public virtual FactorizationResult Process(BigInteger n, int threadCount = 1)
        {
            Cancel(); // Cancel all tasks before start new

            if (threadCount <= 0)
                throw new ArgumentException("Number of threads cannot be less or equal than 0", nameof(threadCount));

            cancellationTokenSource = new CancellationTokenSource();

            Task<FactorizationResult>[] tasks = Enumerable.Range(1, threadCount)
                .Select(i => Task.Run(() => Process(n, i, threadCount, cancellationTokenSource.Token)))
                .ToArray();

            int taskIndex = Task.WaitAny(tasks);
            Cancel();

            return tasks[taskIndex].Result;
        }

        public virtual Task<FactorizationResult> ProcessAsync(BigInteger n, int threadCount = 1)
        {
            try
            {
                return Task.Run(() => Process(n, threadCount));
            }
            catch (OperationCanceledException)
            {
                return Task.FromResult(FactorizationResult.Failed);
            }
        }

        public virtual void Cancel()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
            cancellationTokenSource = null;
        }

        protected abstract FactorizationResult Process(BigInteger n, int threadNumber, int threadCount,
            CancellationToken cancellationToken);
    }
}