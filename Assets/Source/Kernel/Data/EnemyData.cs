using Source.Kernel.Contracts;
using Source.Kernel.Entities;
using UnityEngine;

namespace Source.Kernel.Data
{
    [CreateAssetMenu(menuName = "Data/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private EnemyEntity entity;

        public float Speed => entity.Speed;
        public float MaxHealth => entity.MaxHealth;
        public ResistAmount Resist => entity.Resist;
        public DamageAmount BaseDamage => entity.BaseDamage;
        public int ProtectorateDamage => entity.ProtectorateDamage;
        public float AttackFrequency => entity.AttackFrequency;
    }
}