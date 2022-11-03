using Source.Kernel.Contracts;
using Source.Kernel.Entities;
using UnityEngine;

namespace Source.Kernel.Data
{
    [CreateAssetMenu(menuName = "Data/TowerData")]
    public class TowerData : ScriptableObject
    {
        [SerializeField] private TowerEntity entity;

        public float AttackRange => entity.AttackRange;
        public float AttackRate => entity.AttackRate;
        public DamageAmount Damage => entity.Damage;
    }
}