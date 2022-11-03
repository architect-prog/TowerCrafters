using System;
using Source.Kernel.Contracts;
using UnityEngine;

namespace Source.Kernel.Entities
{
    [Serializable]
    public struct CharacterEntity
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float speed;
        [SerializeField] private float maxMana;
        [SerializeField] private ResistAmount resist;
        [SerializeField] private DamageAmount baseDamage;
        [SerializeField] private float collectingRange;
        [SerializeField] private float attackFrequency;

        public float Speed
        {
            readonly get => speed;
            set => speed = value;
        }

        public float MaxMana
        {
            readonly get => maxMana;
            set => maxMana = value;
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

        public float CollectingRange
        {
            readonly get => collectingRange;
            set => collectingRange = value;
        }

        public float AttackFrequency
        {
            readonly get => attackFrequency;
            set => attackFrequency = value;
        }
    }
}