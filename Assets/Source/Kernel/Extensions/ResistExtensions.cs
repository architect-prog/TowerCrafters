using System.Collections.Generic;
using System.Linq;
using Source.Kernel.Contracts;

namespace Source.Kernel.Extensions
{
    public static class ResistExtensions
    {
        public static Resist[] Aggregate(this IEnumerable<Resist> resist)
        {
            var totalResist = resist
                .GroupBy(x => x.Type)
                .Select(x => new Resist(x.Sum(y => y.ResistPercent), x.Key))
                .ToArray();

            return totalResist;
        }
    }
}