using Source.Core.Components.StatusEffects.Interfaces;
using UnityEngine;

namespace Source.Core.Components.StatusEffects
{
    public class StatusEffectHolder : IStatusEffectHolder<IStatusEffect>
    {
        private readonly IStatusEffectHolder<IPermanentStatusEffect> permanentStatusEffectHolder;
        private readonly IStatusEffectHolder<ITemporaryStatusEffect> temporaryStatusEffectHolder;

        public StatusEffectHolder(MonoBehaviour owner)
        {
            permanentStatusEffectHolder = new PermanentStatusEffectHolder();
            temporaryStatusEffectHolder = new TemporaryStatusEffectHolder(owner);
        }

        public void Enable()
        {
            permanentStatusEffectHolder.Enable();
            temporaryStatusEffectHolder.Enable();
        }

        public void Disable()
        {
            permanentStatusEffectHolder.Disable();
            temporaryStatusEffectHolder.Disable();
        }

        public void Add(IStatusEffect statusEffect)
        {
            switch (statusEffect)
            {
                case IPermanentStatusEffect permanentStatusEffect:
                    permanentStatusEffectHolder.Add(permanentStatusEffect);
                    break;
                case ITemporaryStatusEffect temporaryStatusEffect:
                    temporaryStatusEffectHolder.Add(temporaryStatusEffect);
                    break;
            }
        }

        public void Remove(IStatusEffect statusEffect)
        {
            switch (statusEffect)
            {
                case IPermanentStatusEffect permanentStatusEffect:
                    permanentStatusEffectHolder.Remove(permanentStatusEffect);
                    break;
                case ITemporaryStatusEffect temporaryStatusEffect:
                    temporaryStatusEffectHolder.Remove(temporaryStatusEffect);
                    break;
            }
        }
    }
}