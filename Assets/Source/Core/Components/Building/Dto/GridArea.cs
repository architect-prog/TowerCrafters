using Source.Core.Components.Building.Factories;
using Source.Kernel.Entities;
using UnityEngine;

namespace Source.Core.Components.Building.Dto
{
    public class GridArea
    {
        public Vector2Int Size { get; }
        public Vector2Int LocalGridPosition { get; }
        public GridOptions Options { get; }
        public Cell[] Cells => GridAreaShapeFactory.Create(this).Shape;

        public GridArea(
            Vector2Int size,
            Vector2Int localGridPosition,
            GridOptions options)
        {
            Size = size;
            LocalGridPosition = localGridPosition;
            Options = options;
        }
    }
}