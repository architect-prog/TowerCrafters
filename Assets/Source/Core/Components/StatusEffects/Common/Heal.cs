using Source.Core.Components.StatusEffects.Interfaces;
using Source.Kernel.Interfaces.Components;

namespace Source.Core.Components.StatusEffects.Common
{
    public class Heal : IStatusEffect
    {
        private readonly float amount;
        private readonly IHealthComponent health;

        public Heal(float amount, IHealthComponent health)
        {
            this.health = health;
            this.amount = amount;
        }

        public void Apply()
        {
            health.Heal(amount);
        }

        public void Cancel()
        {
        }
    }
}