using Source.Common.AI.Interfaces;
using Source.Common.DI;
using Source.Common.Editor;
using Source.Core.Components.Units.Common;
using Source.Core.Components.Waypoints;
using Source.Core.Components.Waypoints.Abstractions;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Units.Enemies
{
    public class WaypointMovingComponent :
        MovingComponent,
        ITargetMovingComponent,
        ITargetProvider
    {
        [SerializeField] private float targetChangeDistance;
        [ReadOnly] [SerializeField] private Waypoint target;
        [ReadOnly] [SerializeField] private BaseWaypointCollection path;

        private IRotatingComponent rotating;

        public GameObject Target => target.gameObject;

        [Construct]
        public void Construct(IRotatingComponent rotatingComponent)
        {
            rotating = rotatingComponent;
        }

        private void Start()
        {
            if (!path)
                return;

            target = path.First;
        }

        public void UpdatePath(BaseWaypointCollection waypointCollection)
        {
            path = waypointCollection;
        }

        public void Move()
        {
            if (!target)
                return;

            var movementDirection = target.PointPosition - transform.position;
            Move(movementDirection);
            rotating.Rotate(movementDirection);

            if (Vector2.Distance(transform.position, target.PointPosition) <= targetChangeDistance)
            {
                target = path.GetNextOrDefault(target);
            }
        }
    }
}