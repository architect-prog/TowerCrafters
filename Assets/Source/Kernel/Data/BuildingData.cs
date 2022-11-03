using Source.Kernel.Entities;
using UnityEngine;

namespace Source.Kernel.Data
{
    [CreateAssetMenu(menuName = "Data/BuildingData")]
    public class BuildingData : ScriptableObject
    {
        [SerializeField] private BuildingEntity entity;

        public Vector2Int Size => entity.Size;
        public Vector2Int RootOffset => entity.RootOffset;
    }
}