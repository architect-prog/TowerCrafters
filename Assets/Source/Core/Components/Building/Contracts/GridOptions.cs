using System;
using UnityEngine;

namespace Source.Core.Components.Building.Contracts
{
    [Serializable]
    public class GridOptions
    {
        [SerializeField] private Vector2Int size;
        [SerializeField] private Vector2Int cellSize;

        public Vector2Int Size => size;
        public Vector2Int CellSize => cellSize;
    }
}