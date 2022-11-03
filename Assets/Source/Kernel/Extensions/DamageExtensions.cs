using System.Collections.Generic;
using System.Linq;
using Source.Kernel.Contracts;

namespace Source.Kernel.Extensions
{
    public static class DamageExtensions
    {
        public static Damage[] Aggregate(this IEnumerable<Damage> damage)
        {
            var totalDamage = damage
                .GroupBy(x => x.Type)
                .Select(x => new Damage(x.Sum(y => y.Amount), x.Key))
                .ToArray();

            return totalDamage;
        }
    }
}