using Source.Kernel.Entities;
using UnityEngine;

namespace Source.Kernel.Data
{
    [CreateAssetMenu(menuName = "Data/AbilityData")]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] private AbilityEntity entity;

        public Sprite Icon => entity.Icon;
        public string Name => entity.Name;
        public string Description => entity.Description;
        public float MaxCooldown => entity.MaxCooldown;
    }
}