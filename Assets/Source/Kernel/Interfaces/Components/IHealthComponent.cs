using System;
using Source.Kernel.Contracts;

namespace Source.Kernel.Interfaces.Components
{
    public interface IHealthComponent
    {
        event Action died;
        event Action healed;
        event Action damageTaken;
        float RemainHealth { get; }
        float MaxHealth { get; }
        float RemainHealthPercent { get; }
        void ApplyDamage(DamageAmount damage);
        void Heal(float heal);
    }
}