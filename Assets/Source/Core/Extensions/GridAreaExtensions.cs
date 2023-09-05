using System.Linq;
using Source.Core.Components.Building.Contracts;
using Source.Kernel.Entities;

namespace Source.Core.Extensions
{
    public static class GridAreaExtensions
    {
        public static bool IsInsideGrid(this GridArea gridArea, Cell[] gridCells)
        {
            var isInside = gridArea.Cells.All(x => gridCells.Any(y => x == y));
            return isInside;
        }
    }
}