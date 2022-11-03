using System.Collections.Generic;
using System.Linq;
using Source.Common.Messaging;
using Source.Core.Components.Building;
using Source.Core.Constants;
using Source.UI.Views;
using UnityEngine;

namespace Source.UI.Controllers
{
    [RequireComponent(typeof(BuildingArea))]
    public class BuildingZonesController : MonoBehaviour
    {
        private BuildingArea buildingArea;
        private IEnumerable<BuildingZoneView> zones;

        private void Start()
        {
            buildingArea = GetComponent<BuildingArea>();
            zones = GetComponentsInChildren<BuildingZoneView>() ?? Enumerable.Empty<BuildingZoneView>();

            foreach (var zone in zones)
            {
                zone.Initialize(buildingArea);
                zone.Disable();
            }
        }

        public void EnableZones()
        {
            foreach (var zone in zones)
            {
                zone.Enable();
            }
        }

        public void DisableZones()
        {
            foreach (var zone in zones)
            {
                zone.Disable();
            }
        }

        private void OnEnable()
        {
            MessageBus.AddListener(Messaging.Events.BuildingSelected, () => EnableZones());
            MessageBus.AddListener(Messaging.Events.BuildingSelectionCanceled, () => DisableZones());
        }

        private void OnDisable()
        {
            MessageBus.RemoveListener(Messaging.Events.BuildingSelected, () => EnableZones());
            MessageBus.RemoveListener(Messaging.Events.BuildingSelectionCanceled, () => DisableZones());
        }
    }
}