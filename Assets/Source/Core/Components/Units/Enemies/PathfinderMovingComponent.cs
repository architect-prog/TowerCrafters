using System.Collections.Generic;
using Pathfinding;
using Source.Common.AI.Sensors.Interfaces;
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
        private Vector2 startPosition;

        public GameObject Target => character.gameObject;
        public IEnumerable<GameObject> Targets { get; }

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
                    startPosition = transform.position;
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
            var nextTarget = path.vectorPath.Count <= currentWaypoint + 1
                ? path.vectorPath[currentWaypoint + 1]
                : Vector3.zero;

            var currentPosition = transform.position;
            if (nextTarget != Vector3.zero)
            {
               var points = GetSomething(startPosition, target, nextTarget);
               target = points[1];
            }
            Debug.DrawLine(currentPosition, target, Color.cyan);

            var movementDirection = target - currentPosition;
            Move(movementDirection);
            rotating.Rotate(movementDirection);

            if (Vector2.Distance(currentPosition, path.vectorPath[currentWaypoint]) <= targetChangeDistance)
            {
                currentWaypoint++;
            }
        }


        // private void OnDrawGizmos()
        // {
        //     var start1 = new Vector2(1, 5);
        //     var end1 = new Vector2(2, 6);
        //     var start2 = new Vector2(2, 6);
        //     var end2 = new Vector2(1, 8);
        //
        //     Gizmos.color = Color.blue;
        //     Gizmos.DrawLine(start1, end1);
        //     Gizmos.DrawSphere(end1, 0.2f);
        //     Gizmos.DrawLine(start2, end2);
        //     Gizmos.color = Color.magenta;
        //
        //     var something = GetSomething(start1, end1, end2);
        //     for (var i = 0; i < something.Length - 1; i++)
        //     {
        //         Gizmos.DrawLine(something[i], something[i + 1]);
        //     }
        // }

        private Vector2 QuadraticBezierCurves(Vector2 start, Vector2 middle, Vector2 end,  float x)
        {
            x = Mathf.Clamp01(x);
            var result = Mathf.Pow(1 - x, 2) * start + 2 * (1 - x) * x * middle + x * x * end;
            return result;
        }

        private Vector2[] GetSomething(Vector2 start, Vector2 middle, Vector2 end)
        {
            var numOfSubs = 4;
            var result = new Vector2[numOfSubs];

            for (var i = 0; i < numOfSubs; i++)
            {
                var x = (float) i / numOfSubs;
                result[i] = QuadraticBezierCurves(start, middle, end, x);
            }

            return result;
        }
    }
}