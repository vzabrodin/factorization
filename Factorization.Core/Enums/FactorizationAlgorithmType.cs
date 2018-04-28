using System.ComponentModel.DataAnnotations;

namespace Factorization.Core.Enums
{
    public enum FactorizationAlgorithmType
    {
        [Display(Description = "Quadratic inequality")]
        QuadraticInequality = 1,

        [Display(Description = "Greatest common divisor")]
        GreatestCommonDivisor = 2,

        [Display(Description = "Quadratic sieve")]
        QuadraticSieve = 3
    }
}