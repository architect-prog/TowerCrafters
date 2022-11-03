using Source.Kernel.Entities;
using UnityEngine;

namespace Source.Core.Components.Building.Interfaces
{
    public interface IBuildingArea
    {
        Cell[] GetFilledCells();

        Cell[] GetFreeCells();

        bool CanBuild(Building building, Vector2 buildPosition);

        void Build(Building building, Vector2 buildPosition);
    }
}