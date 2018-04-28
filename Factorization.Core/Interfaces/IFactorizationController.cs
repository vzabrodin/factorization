using System.Numerics;
using System.Threading.Tasks;

namespace Factorization.Core.Interfaces
{
    public interface IFactorizationController
    {
        FactorizationResult Process(BigInteger n, int threadCount = 1);

        Task<FactorizationResult> ProcessAsync(BigInteger n, int threadCount = 1);

        void Cancel();
    }
}