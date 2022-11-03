using Source.Kernel.Contracts;
using Source.Kernel.Entities;
using UnityEngine;

namespace Source.Kernel.Data
{
    [CreateAssetMenu(menuName = "Data/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private CharacterEntity entity;

        public float Speed => entity.Speed;
        public float MaxMana => entity.MaxMana;
        public float MaxHealth => entity.MaxHealth;
        public ResistAmount Resist => entity.Resist;
        public DamageAmount BaseDamage => entity.BaseDamage;
        public float AttackFrequency => entity.AttackFrequency;
        public float CollectingRange => entity.CollectingRange;
    }
}