using System.Collections.Generic;
using System.Linq;
using Source.Common.Utils;
using Source.Core.Components.StatusEffects.Interfaces;
using UnityEngine;

namespace Source.Core.Components.StatusEffects
{
    public class TemporaryStatusEffectHolder : IStatusEffectHolder<ITemporaryStatusEffect>
    {
        private readonly RepeatingTimer repeatingTimer;
        private readonly List<ITemporaryStatusEffect> statusEffects;

        private bool isEnabled;

        public TemporaryStatusEffectHolder(MonoBehaviour owner)
        {
            statusEffects = new List<ITemporaryStatusEffect>();
            repeatingTimer = new RepeatingTimer(owner)
                .WithAction(() => UpdateStatusEffects())
                .InvokeImmediately(true);
        }

        public void Enable()
        {
            foreach (var statusEffect in statusEffects)
            {
                statusEffect.Apply();
            }

            repeatingTimer.Start();
            isEnabled = true;
        }

        public void Disable()
        {
            foreach (var statusEffect in statusEffects)
            {
                statusEffect.Cancel();
            }

            repeatingTimer.Stop();
            isEnabled = false;
        }

        public void Add(ITemporaryStatusEffect statusEffect)
        {
            if (isEnabled)
                statusEffect.Apply();

            statusEffects.Add(statusEffect);
        }

        public void Remove(ITemporaryStatusEffect statusEffect)
        {
            if (isEnabled)
                statusEffect.Cancel();

            statusEffects.Remove(statusEffect);
        }

        private void UpdateStatusEffects()
        {
            var deltaTime = Time.deltaTime;
            foreach (var statusEffect in statusEffects)
            {
                statusEffect.RemainingDuration -= deltaTime;
            }

            var canceledStatusEffects = statusEffects
                .Where(x => x.RemainingDuration <= 0)
                .ToArray();

            foreach (var statusEffect in canceledStatusEffects)
            {
                Remove(statusEffect);
            }
        }
    }
}