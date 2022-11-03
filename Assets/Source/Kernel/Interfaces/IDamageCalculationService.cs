using Source.Kernel.Contracts;

namespace Source.Kernel.Interfaces
{
    public interface IDamageCalculationService
    {
        float CalculateResultDamage(DamageAmount damage, ResistAmount resist);
    }
}