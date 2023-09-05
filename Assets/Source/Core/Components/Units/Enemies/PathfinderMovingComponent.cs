using Pathfinding;
using Source.Common.AI.Interfaces;
using Source.Common.DI;
using Source.Core.Components.Units.Characters;
using Source.Core.Components.Units.Common;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Units.Enemies
{
    [RequireComponent(typeof(Seeker))]
    public class PathfinderMovingComponent :
        MovingComponent,
        ITargetMovingComponent,
        ITargetProvider
    {
        [SerializeField] private float targetChangeDistance;

        private Path path;
        private Seeker seeker;
        private Character character;
        private IRotatingComponent rotating;

        private int currentWaypoint;

        public GameObject Target => character.gameObject;

        [Construct]
        public void Construct(IRotatingComponent rotatingComponent)
        {
            rotating = rotatingComponent;
            seeker = GetComponent<Seeker>();
            character = FindFirstObjectByType<Character>();

            InvokeRepeating(nameof(CalculatePath), 0, 0.5f);
        }

        private void CalculatePath()
        {
            seeker.StartPath(transform.position, character.transform.position, (calculatedPath) =>
            {
                if (!calculatedPath.error)
                {
                    path = calculatedPath;
                    currentWaypoint = 0;
                }
            });
        }

        public void Move()
        {
            if (!character)
                return;

            if (path is null || path.vectorPath.Count <= currentWaypoint)
                return;

            var target = path.vectorPath[currentWaypoint];
            var movementDirection = target - transform.position;
            Move(movementDirection);
            rotating.Rotate(movementDirection);

            if (Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]) <= targetChangeDistance)
            {
                currentWaypoint++;
            }
        }
    }
}