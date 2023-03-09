using System;
using System.Linq;
using Source.Core.Components.Building.Dto;
using Source.Core.Components.Building.Interfaces;
using Source.Kernel.Entities;
using UnityEngine;

namespace Source.Core.Components.Building
{
    [RequireComponent(typeof(Grid))]
    public class BuildingArea : MonoBehaviour, IBuildingArea
    {
        private Grid grid;

        private void Start()
        {
            grid = GetComponent<Grid>();
        }

        public Cell[] GetFilledCells()
        {
            var result = grid.Cells.Where(x => !x.IsFree).ToArray();
            return result;
        }

        public Cell[] GetFreeCells()
        {
            var result = grid.Cells.Where(x => x.IsFree).ToArray();
            return result;
        }

        public bool CanBuild(Building building, Vector2 buildPosition)
        {
            if (building is null)
                return false;

            var area = GetGridArea(building, buildPosition);
            var result = grid.CanFillArea(area);

            return result;
        }

        public void Build(Building building, Vector2 buildPosition)
        {
            if (building is null)
                throw new ArgumentNullException(nameof(building));

            var area = GetGridArea(building, buildPosition);

            if (grid.TryFillArea(area))
            {
                var buildingPosition = grid.RootPosition + area.LocalGridPosition + building.Data.RootOffset;
                Instantiate(building, buildingPosition, Quaternion.identity);
            }
        }

        private GridArea GetGridArea(Building building, Vector2 buildPosition)
        {
            var localGridPosition = Vector2Int.FloorToInt(buildPosition - grid.RootPosition);
            var result = new GridArea(building.Data.Size, localGridPosition, grid.Options);

            return result;
        }
    }
}