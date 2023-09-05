using System;
using System.Collections.Generic;
using System.Linq;
using Source.Common.Utils;
using Source.Core.Components.Building.Contracts;
using Source.Core.Components.Building.Factories;
using Source.Core.Components.Building.Interfaces;
using Source.Core.Constants;
using Source.Core.Extensions;
using Source.Kernel.Entities;
using UnityEngine;

namespace Source.Core.Components.Building
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private GridOptions options;

        private Cell[] cells;
        private IGridShapeFactory gridShapeFactory;

        public GridOptions Options => options;
        public Vector2 RootPosition => transform.position;
        public IReadOnlyCollection<Cell> Cells => cells;

        private void Start()
        {
            gridShapeFactory = RectangleGridShapeFactory.Create(options);
            cells = gridShapeFactory.Shape;
        }

        public bool CanFillArea(GridArea gridArea)
        {
            var areaCells = GetGridAreaCells(gridArea);
            var result = areaCells.Any() && areaCells.All(x => x.IsFree);

            return result;
        }

        public bool TryFillArea(GridArea gridArea)
        {
            var areaCells = GetGridAreaCells(gridArea);
            if (areaCells.Any() && areaCells.Any(x => !x.IsFree))
                return false;

            foreach (var cell in areaCells)
            {
                cell.FillCell();
            }

            return true;
        }

        public bool TryFreeArea(GridArea gridArea)
        {
            var areaCells = GetGridAreaCells(gridArea);
            foreach (var cell in areaCells)
            {
                cell.FreeCell();
            }

            return true;
        }

        private Cell[] GetGridAreaCells(GridArea gridArea)
        {
            if (!gridArea.IsInsideGrid(cells))
            {
                return Array.Empty<Cell>();
            }

            var result = cells
                .Where(x => gridArea.Cells.Any(y => x.Equals(y)))
                .ToArray();

            return result;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Colors.SemitransparentWhite;

            var drawableCells = cells ?? RectangleGridShapeFactory.Create(options).Shape;
            foreach (var cell in drawableCells)
            {
                var startPosition = RootPosition + cell.Position;
                GizmosUtils.DrawRect(startPosition, options.CellSize);
                if (!cell.IsFree)
                {
                    Gizmos.DrawSphere(startPosition + options.CellSize.ToVector2() / 2, 0.2f);
                }
            }
        }
    }
}