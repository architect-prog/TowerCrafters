using Source.Kernel.Contracts;

namespace Source.Kernel.Interfaces.Providers
{
    public interface IDamageProvider
    {
        DamageAmount TotalDamage { get; }
    }
}