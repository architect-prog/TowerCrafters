using Source.Core.Components.StatusEffects.Interfaces;
using Source.Kernel.Contracts;

namespace Source.Core.Components.StatusEffects.Common
{
    public class MaxHealthStatusEffect : IPermanentStatusEffect
    {
        private readonly float effectValue;
        private readonly ModifiableStat<float> health;

        private Effect<float> effect;

        public MaxHealthStatusEffect(
            float effectValue,
            ModifiableStat<float> health)
        {
            this.effectValue = effectValue;
            this.health = health;
        }

        public void Apply()
        {
            effect = new Effect<float>(effectValue);
            health.AddEffect(effect);
        }

        public void Cancel()
        {
            health.RemoveEffect(effect);
            effect = null;
        }
    }
}