using System;
using System.Collections.Generic;
using Source.Core.Components.Building.Contracts;
using Source.Core.Components.Building.Interfaces;
using Source.Kernel.Entities;
using UnityEngine;

namespace Source.Core.Components.Building.Factories
{
    public class GridAreaShapeFactory : IGridShapeFactory
    {
        private readonly GridArea area;
        private readonly Lazy<Cell[]> shape;

        private Vector2Int CellSize => area.Options.CellSize;
        private Vector2Int StartPosition => area.LocalGridPosition;

        public Cell[] Shape => shape.Value;

        public GridAreaShapeFactory(GridArea area)
        {
            this.area = area;
            shape = new Lazy<Cell[]>(() => GetShape());
        }

        private Cell[] GetShape()
        {
            var result = new List<Cell>();
            for (var x = StartPosition.x; x < StartPosition.x + area.Size.x * CellSize.x; x += CellSize.x)
            {
                for (var y = StartPosition.y; y < StartPosition.y + area.Size.y * CellSize.y; y += CellSize.y)
                {
                    result.Add(new Cell(x, y));
                }
            }

            return result.ToArray();
        }

        public static IGridShapeFactory Create(GridArea area)
        {
            var result = new GridAreaShapeFactory(area);
            return result;
        }
    }
}