using System;
using System.Collections.Generic;
using System.Linq;
using Source.Kernel.Extensions;
using UnityEngine;

namespace Source.Kernel.Contracts
{
    [Serializable]
    public class ResistAmount
    {
        [SerializeField] private Resist[] resistItems;

        public IReadOnlyCollection<Resist> ResistItems => resistItems;

        public ResistAmount(params Resist[] resist)
        {
            resistItems = resist;
        }

        public static ResistAmount operator +(ResistAmount first, ResistAmount second)
        {
            if (first is null)
                throw new ArgumentNullException(nameof(first));

            if (second is null)
                throw new ArgumentNullException(nameof(second));

            var totalResist = first.ResistItems
                .Concat(second.ResistItems)
                .Aggregate();

            var result = new ResistAmount(totalResist);
            return result;
        }

        public static ResistAmount operator -(ResistAmount first, ResistAmount second)
        {
            if (first is null)
                throw new ArgumentNullException(nameof(first));

            if (second is null)
                throw new ArgumentNullException(nameof(second));

            var subtrahend = second.ResistItems
                .Aggregate();

            var totalResist = first.ResistItems
                .Aggregate()
                .Select(x => new Resist(x.ResistPercent - subtrahend.FirstOrDefault(y => y.Type == x.Type).ResistPercent, x.Type))
                .ToArray();

            var result = new ResistAmount(totalResist);
            return result;
        }
    }
}