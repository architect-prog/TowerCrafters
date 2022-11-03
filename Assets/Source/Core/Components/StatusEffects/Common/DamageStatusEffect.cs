using Source.Core.Components.StatusEffects.Interfaces;
using Source.Kernel.Contracts;

namespace Source.Core.Components.StatusEffects.Common
{
    public class DamageStatusEffect : IPermanentStatusEffect
    {
        private readonly DamageAmount effectValue;
        private readonly ModifiableStat<DamageAmount> damage;

        private Effect<DamageAmount> effect;

        public DamageStatusEffect(
            DamageAmount effectValue,
            ModifiableStat<DamageAmount> damage)
        {
            this.effectValue = effectValue;
            this.damage = damage;
        }

        public void Apply()
        {
            effect = new Effect<DamageAmount>(effectValue);
            damage.AddEffect(effect);
        }

        public void Cancel()
        {
            damage.RemoveEffect(effect);
            effect = null;
        }
    }
}