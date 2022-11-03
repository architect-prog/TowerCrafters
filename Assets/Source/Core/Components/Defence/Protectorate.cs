using System;
using Source.Core.Components.Defence.Interfaces;
using UnityEngine;

namespace Source.Core.Components.Defence
{
    [Serializable]
    public class Protectorate : IProtectorate
    {
        [SerializeField] private int maxDurability;
        private int durability;

        public int Durability => durability;
        public int MaxDurability => maxDurability;

        public void Repair(int amount)
        {
            durability = Durability + amount;
            if (Durability > MaxDurability)
            {
                durability = MaxDurability;
            }
        }

        public void Damage(int damage)
        {
            durability = Durability - damage;
        }
    }
}