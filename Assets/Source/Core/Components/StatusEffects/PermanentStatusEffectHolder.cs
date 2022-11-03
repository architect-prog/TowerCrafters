using System.Collections.Generic;
using Source.Core.Components.StatusEffects.Interfaces;

namespace Source.Core.Components.StatusEffects
{
    public class PermanentStatusEffectHolder : IStatusEffectHolder<IPermanentStatusEffect>
    {
        private readonly List<IPermanentStatusEffect> statusEffects;

        private bool isEnabled;

        public PermanentStatusEffectHolder()
        {
            statusEffects = new List<IPermanentStatusEffect>();
        }

        public void Enable()
        {
            foreach (var statusEffect in statusEffects)
            {
                statusEffect.Apply();
            }
        }

        public void Disable()
        {
            foreach (var statusEffect in statusEffects)
            {
                statusEffect.Cancel();
            }
        }

        public void Add(IPermanentStatusEffect statusEffect)
        {
            if (isEnabled)
                statusEffect.Apply();

            statusEffects.Add(statusEffect);
        }

        public void Remove(IPermanentStatusEffect statusEffect)
        {
            if (isEnabled)
                statusEffect.Cancel();

            statusEffects.Remove(statusEffect);
        }
    }
}