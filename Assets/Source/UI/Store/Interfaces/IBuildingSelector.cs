using Source.Core.Components.Building;

namespace Source.UI.Store.Interfaces
{
    public interface IBuildingSelector
    {
        public Building SelectedBuilding { get; }
        public void SetBuilding(Building building);
        public void Clear();
    }
}