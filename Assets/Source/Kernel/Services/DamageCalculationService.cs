using System;
using System.Linq;
using Source.Kernel.Contracts;
using Source.Kernel.Interfaces;

namespace Source.Kernel.Services
{
    public class DamageCalculationService : IDamageCalculationService
    {
        public float CalculateResultDamage(DamageAmount damage, ResistAmount resist)
        {
            if (damage is null)
                throw new ArgumentNullException(nameof(damage));
            if (resist is null)
                throw new ArgumentNullException(nameof(resist));

            var damageItems = damage.DamageItems ?? Enumerable.Empty<Damage>();
            var resistItems = resist.ResistItems ?? Enumerable.Empty<Resist>();

            var damageResistPairs = damageItems
                .Select(x => (Damage: x, Resist: resistItems.FirstOrDefault(y => y.Type == x.Type)));

            var result = damageResistPairs
                .Sum(x => x.Damage.Amount * (1 - x.Resist.ResistPercent));

            return result;
        }
    }
}