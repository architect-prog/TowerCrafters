using System;
using System.Collections.Generic;
using Source.Core.Components.Building.Contracts;
using Source.Core.Components.Building.Interfaces;
using Source.Kernel.Entities;

namespace Source.Core.Components.Building.Factories
{
    public class RectangleGridShapeFactory : IGridShapeFactory
    {
        private readonly GridOptions options;
        private readonly Lazy<Cell[]> shape;

        public Cell[] Shape => shape.Value;

        public RectangleGridShapeFactory(GridOptions options)
        {
            this.options = options;
            shape = new Lazy<Cell[]>(() => GetShape());
        }

        private Cell[] GetShape()
        {
            var result = new List<Cell>();
            for (var x = 0; x < options.Size.x * options.CellSize.x; x += options.CellSize.x)
            {
                for (var y = 0; y < options.Size.y * options.CellSize.y; y += options.CellSize.y)
                {
                    result.Add(new Cell(x, y));
                }
            }

            return result.ToArray();
        }

        public static IGridShapeFactory Create(GridOptions options)
        {
            var result = new RectangleGridShapeFactory(options);
            return result;
        }
    }
}