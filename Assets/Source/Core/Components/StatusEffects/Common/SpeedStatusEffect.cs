using Source.Core.Components.StatusEffects.Interfaces;
using Source.Kernel.Contracts;

namespace Source.Core.Components.StatusEffects.Common
{
    public class SpeedStatusEffect : IPermanentStatusEffect
    {
        private readonly float effectValue;
        private readonly ModifiableStat<float> speed;

        private Effect<float> effect;

        public SpeedStatusEffect(
            float effectValue,
            ModifiableStat<float> speed)
        {
            this.effectValue = effectValue;
            this.speed = speed;
        }

        public void Apply()
        {
            effect = new Effect<float>(effectValue);
            speed.AddEffect(effect);
        }

        public void Cancel()
        {
            speed.RemoveEffect(effect);
            effect = null;
        }
    }
}