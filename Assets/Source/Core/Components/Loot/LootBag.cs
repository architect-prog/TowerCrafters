using System.Collections.Generic;
using System.Linq;
using Source.Common.Utils;
using Source.Core.Constants;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Loot
{
    public class LootBag : MonoBehaviour, ILootBag
    {
        [SerializeField] private float dropRadius;
        [SerializeField] private List<LootItem> loot;

        public void DropLoot()
        {
            var droppedLoot = loot
                .Where(x => RandomUtils.IsDropped(x.DropChance))
                .ToArray();

            var currentPosition = transform.position;
            foreach (var drop in droppedLoot)
            {
                var positions = Enumerable
                    .Range(0, drop.DropCount)
                    .Select(_ => RandomUtils.InsideCircle(currentPosition, dropRadius))
                    .ToArray();

                ApplicationRoot.Instance.ObjectCreator.CreateMany(drop.Collectable, positions);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Colors.SemitransparentYellow;
            Gizmos.DrawWireSphere(transform.position, dropRadius);
        }
    }
}