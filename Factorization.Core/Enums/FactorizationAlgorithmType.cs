using System.ComponentModel.DataAnnotations;

namespace Factorization.Core.Enums
{
    public enum FactorizationAlgorithmType
    {
        [Display(Description = "Pevnev")]
        Pevnev = 1,

        [Display(Description = "Greatest common divisor ")]
        GreatestCommonDivisor = 2,

        [Display(Description = "Quadratic sieve ")]
        QuadraticSieve = 3
    }
}