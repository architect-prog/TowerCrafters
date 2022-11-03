using Source.Core.Components.Building;
using UnityEngine;

namespace Source.UI.Controllers
{
    public class BuildingSelectionController : MonoBehaviour
    {
        public void SelectBuilding(Building building)
        {
            ApplicationStore.Instance.BuildingStore.SetBuilding(building);
        }
    }
}