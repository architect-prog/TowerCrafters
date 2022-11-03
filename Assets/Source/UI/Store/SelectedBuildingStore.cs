using System;
using Source.Common.Messaging;
using Source.Core.Components.Building;
using Source.Core.Constants;
using Source.UI.Store.Interfaces;

namespace Source.UI.Store
{
    public class SelectedBuildingStore : IBuildingSelector
    {
        private Building selectedBuilding;

        public Building SelectedBuilding => selectedBuilding;

        public void SetBuilding(Building building)
        {
            if (building is null)
                throw new ArgumentNullException(nameof(building));

            selectedBuilding = building;
            MessageBus.Broadcast(Messaging.Events.BuildingSelected);
        }

        public void Clear()
        {
            selectedBuilding = null;
            MessageBus.Broadcast(Messaging.Events.BuildingSelectionCanceled);
        }
    }
}