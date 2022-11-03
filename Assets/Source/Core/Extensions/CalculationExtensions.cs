using Source.Kernel.Contracts;

namespace Source.Core.Extensions
{
    public static class CalculationExtensions
    {
        public static float CalculateDamage(this DamageAmount damage, ResistAmount resist)
        {
            var result = ApplicationRoot.Instance.DamageCalculationService.CalculateResultDamage(damage, resist);
            return result;
        }
    }
}