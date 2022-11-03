using System;
using System.Collections.Generic;
using System.Linq;
using Source.Kernel.Extensions;
using UnityEngine;

namespace Source.Kernel.Contracts
{
    [Serializable]
    public class DamageAmount
    {
        [SerializeField] private Damage[] damageItems;

        public IReadOnlyCollection<Damage> DamageItems => damageItems;

        public DamageAmount(params Damage[] damage)
        {
            damageItems = damage.Aggregate();
        }

        public static DamageAmount operator +(DamageAmount first, DamageAmount second)
        {
            if (first is null)
                throw new ArgumentNullException(nameof(first));

            if (second is null)
                throw new ArgumentNullException(nameof(second));

            var totalDamage = first.DamageItems
                .Concat(second.DamageItems)
                .Aggregate();

            var result = new DamageAmount(totalDamage);
            return result;
        }

        public static DamageAmount operator -(DamageAmount first, DamageAmount second)
        {
            if (first is null)
                throw new ArgumentNullException(nameof(first));

            if (second is null)
                throw new ArgumentNullException(nameof(second));

            var subtrahend = second.DamageItems
                .Aggregate();

            var totalDamage = first.DamageItems
                .Aggregate()
                .Select(x => new Damage(x.Amount - subtrahend.FirstOrDefault(y => y.Type == x.Type).Amount, x.Type))
                .ToArray();

            var result = new DamageAmount(totalDamage);
            return result;
        }
    }
}