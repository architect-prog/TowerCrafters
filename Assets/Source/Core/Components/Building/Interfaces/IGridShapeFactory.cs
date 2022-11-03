using Source.Kernel.Entities;

namespace Source.Core.Components.Building.Interfaces
{
    public interface IGridShapeFactory
    {
        public Cell[] Shape { get; }
    }
}