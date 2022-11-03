using System;
using Source.Common.Utils;
using Source.Core.Components.Loot.Abstractions;
using UnityEngine;

namespace Source.Core.Components.Loot
{
    [Serializable]
    public class LootItem
    {
        [SerializeField] private int minDropCount;
        [SerializeField] private int maxDropCount;
        [SerializeField] private BaseCollectable collectable;
        [SerializeField] [Range(0, 1)] private float dropChance;

        public float DropChance => dropChance;
        public BaseCollectable Collectable => collectable;
        public int DropCount => RandomUtils.GetDropCount(minDropCount, maxDropCount);
    }
}