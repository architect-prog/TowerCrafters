using Source.Common.Utils;
using Source.Core.Components.StatusEffects.Interfaces;
using UnityEngine;

namespace Source.Core.Components.StatusEffects.Common
{
    public class RepeatingStatusEffect : ITemporaryStatusEffect
    {
        private readonly RepeatingTimer repeatingTimer;

        public float Duration { get; }
        public float RemainingDuration { get; set; }

        public RepeatingStatusEffect(
            int ticks,
            float duration,
            IStatusEffect statusEffect,
            MonoBehaviour owner)
        {
            var ticksDelay = Duration / ticks;
            repeatingTimer = new RepeatingTimer(owner)
                .InvokeImmediately(true)
                .WithAction(() => statusEffect.Apply())
                .WithFrequency(ticksDelay);

            Duration = duration;
            RemainingDuration = duration;
        }

        public void Apply()
        {
            repeatingTimer.Start();
        }

        public void Cancel()
        {
            repeatingTimer.Stop();
        }
    }
}