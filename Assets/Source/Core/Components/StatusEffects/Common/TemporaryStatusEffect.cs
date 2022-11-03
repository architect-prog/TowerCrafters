using Source.Core.Components.StatusEffects.Interfaces;

namespace Source.Core.Components.StatusEffects.Common
{
    public class TemporaryStatusEffect : ITemporaryStatusEffect
    {
        private readonly IPermanentStatusEffect statusEffect;

        public float Duration { get; }
        public float RemainingDuration { get; set; }

        public TemporaryStatusEffect(
            float duration,
            IPermanentStatusEffect statusEffect)
        {
            this.statusEffect = statusEffect;

            Duration = duration;
            RemainingDuration = duration;
        }

        public void Apply()
        {
            statusEffect.Apply();
        }

        public void Cancel()
        {
            statusEffect.Cancel();
        }
    }
}