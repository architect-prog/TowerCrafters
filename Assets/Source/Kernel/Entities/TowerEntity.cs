using System;
using Source.Kernel.Contracts;
using UnityEngine;

namespace Source.Kernel.Entities
{
    [Serializable]
    public struct TowerEntity
    {
        [SerializeField] private float attackRange;
        [SerializeField] private float attackRate;
        [SerializeField] private DamageAmount damage;

        public float AttackRange
        {
            get => attackRange;
            set => attackRange = value;
        }

        public float AttackRate
        {
            get => attackRate;
            set => attackRate = value;
        }

        public DamageAmount Damage
        {
            get => damage;
            set => damage = value;
        }
    }
}