using System;
using System.Collections.Generic;
using System.Linq;

namespace Source.Kernel.Contracts
{
    public class ModifiableStat<T>
    {
        private readonly Func<T, T, T> aggregator;
        private readonly List<Effect<T>> effects;

        public T BaseValue { get; }
        public T ModifiedValue { get; private set; }

        public ModifiableStat(T baseValue, Func<T, T, T> aggregator)
        {
            this.aggregator = aggregator ?? throw new ArgumentNullException(nameof(aggregator));

            BaseValue = baseValue;
            ModifiedValue = baseValue;
            effects = new List<Effect<T>>();
        }

        public void AddEffect(Effect<T> effect)
        {
            effects.Add(effect);
            ModifiedValue = Calculate();
        }

        public void RemoveEffect(Effect<T> effect)
        {
            effects.Remove(effect);
            ModifiedValue = Calculate();
        }

        private T Calculate()
        {
            var result = effects.Aggregate(BaseValue, (current, effect) => aggregator(current, effect.Value));
            return result;
        }
    }
}