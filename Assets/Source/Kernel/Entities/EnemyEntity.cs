using System;
using Source.Kernel.Contracts;
using UnityEngine;

namespace Source.Kernel.Entities
{
    [Serializable]
    public struct EnemyEntity
    {
        [SerializeField] private float speed;
        [SerializeField] private float maxHealth;
        [SerializeField] private ResistAmount resist;
        [SerializeField] private DamageAmount baseDamage;
        [SerializeField] private int protectorateDamage;
        [SerializeField] private float attackFrequency;

        public float Speed
        {
            readonly get => speed;
            set => speed = value;
        }

        public float MaxHealth
        {
            readonly get => maxHealth;
            set => maxHealth = value;
        }

        public ResistAmount Resist
        {
            readonly get => resist;
            set => resist = value;
        }

        public DamageAmount BaseDamage
        {
            readonly get => baseDamage;
            set => baseDamage = value;
        }

        public int ProtectorateDamage
        {
            readonly get => protectorateDamage;
            set => protectorateDamage = value;
        }

        public float AttackFrequency
        {
            readonly get => attackFrequency;
            set => attackFrequency = value;
        }
    }
}