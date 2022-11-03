using Source.Core;
using Source.Core.Components.Building;
using Source.Core.Constants;
using Source.Core.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Source.UI.Views
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class BuildingZoneView : MonoBehaviour, IPointerClickHandler
    {
        private readonly ILogger logger = ApplicationRoot.Instance.Logger;

        [SerializeField] private Color activeColor;
        [SerializeField] private Vector2Int size;

        private bool isFilled;
        private SpriteRenderer sprite;
        private BuildingArea buildingArea;

        public Vector2 Position => transform.position;
        public Vector2 RootPosition => Position - size.ToVector2() / 2;

        public void Initialize(BuildingArea area)
        {
            buildingArea = area;
            sprite = GetComponentInChildren<SpriteRenderer>();
        }

        public void Enable()
        {
            if (isFilled)
            {
                return;
            }
            
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (buildingArea is null)
            {
                var message = string.Format(LoggingConstants.NotAssigned, nameof(BuildingArea), name);
                logger.LogWarning(nameof(BuildingZoneView), message);
                return;
            }

            var building = ApplicationStore.Instance.BuildingStore.SelectedBuilding;
            if (buildingArea.CanBuild(building, RootPosition))
            {
                buildingArea.Build(building, RootPosition);
                ApplicationStore.Instance.BuildingStore.Clear();
                isFilled = true;
            }
        }

        private void OnValidate()
        {
            sprite = GetComponentInChildren<SpriteRenderer>();

            transform.localScale = new Vector3(size.x, size.y, 1);
            sprite.color = activeColor;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(RootPosition, 0.2f);
        }
    }
}