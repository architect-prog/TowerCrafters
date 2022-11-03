using System;
using UnityEngine;

namespace Source.Kernel.Entities
{
    [Serializable]
    public struct BuildingEntity
    {
        [SerializeField] private Vector2Int size;
        [SerializeField] private Vector2Int rootOffset;

        public Vector2Int Size
        {
            readonly get => size;
            set => size = value;
        }

        public Vector2Int RootOffset
        {
            readonly get => rootOffset;
            set => rootOffset = value;
        }
    }
}