using System;
using System.Collections.Generic;
using System.Linq;
using Source.Common.AI;
using Source.Common.AI.Interfaces;
using Source.Common.DI;
using Source.Core.Components.Units.Characters;
using Source.Core.Components.Units.Common;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Units.Enemies
{
    public class ContextSteeringMovingComponent :
        MovingComponent,
        ITargetMovingComponent,
        ITargetProvider
    {
        [SerializeField] private MultiTargetSensor obstacleSensor;

        private Character character;
        private IRotatingComponent rotating;

        private float scanningRadius = 2f;
        private float targetReachDistance = 0.2f;
        private bool targetReached = false;
        private float[] interestWeights = new float[8];
        private float[] dangerWeights = new float[8];
        private Collider2D[] obstacles = Array.Empty<Collider2D>();
        private Vector2 resultDirection = Vector2.zero;
        private List<Vector2> contactPoints = new();

        public GameObject Target => character.gameObject;
        public IEnumerable<GameObject> Targets { get; }

        [Construct]
        public void Construct(IRotatingComponent rotatingComponent)
        {
            rotating = rotatingComponent;
            character = FindFirstObjectByType<Character>();

            obstacleSensor.targetsChanged += targets =>
            {
                obstacles = targets.Select(x => x.GetComponent<Collider2D>()).ToArray();
            };
        }

        public void Move()
        {
            if (!character)
                return;

            for (var i = 0; i < interestWeights.Length; i++)
            {
                dangerWeights[i] = 0;
                interestWeights[i] = 0;
            }

            contactPoints.Clear();

            var currentPosition = (Vector2) transform.position;
            var targetPosition = (Vector2) character.transform.position;

            foreach (var obstacle in obstacles)
            {
                var obstacleDirection = obstacle.ClosestPoint(currentPosition) - currentPosition;
                contactPoints.Add(obstacleDirection);

                var distanceToObstacle = obstacleDirection.magnitude;

                var weight = distanceToObstacle <= scanningRadius
                    ? 1
                    : (scanningRadius - distanceToObstacle) / scanningRadius;

                var normalizedDirectionToObstacle = obstacleDirection.normalized;

                for (var i = 0; i < directions.Length; i++)
                {
                    var result = Vector2.Dot(normalizedDirectionToObstacle, directions[i]);

                    var valueToPutIn = result * weight;
                    if (valueToPutIn > dangerWeights[i])
                    {
                        dangerWeights[i] = valueToPutIn;
                    }
                }
            }

            if (Vector2.Distance(transform.position, targetPosition) < targetReachDistance)
            {
                targetReached = true;
            }

            var directionToTarget = targetPosition - currentPosition;
            for (var i = 0; i < interestWeights.Length; i++)
            {
                var result = Vector2.Dot(directionToTarget.normalized, directions[i]);

                //accept only directions at the less than 90 degrees to the target direction
                if (result > 0)
                {
                    var valueToPutIn = result;
                    if (valueToPutIn > interestWeights[i])
                    {
                        interestWeights[i] = valueToPutIn;
                    }
                }
            }

            for (var i = 0; i < 8; i++)
            {
                interestWeights[i] = Mathf.Clamp01(interestWeights[i] - dangerWeights[i]);
            }

            var outputDirection = Vector2.zero;
            for (var i = 0; i < 8; i++)
            {
                outputDirection += directions[i] * interestWeights[i];
            }

            resultDirection = outputDirection.normalized;

            var movementDirection = resultDirection;
            Move(movementDirection);
            rotating.Rotate(movementDirection);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            var currentPosition = (Vector2) transform.position;
            for (var i = 0; i < directions.Length; i++)
            {
                var first = currentPosition + directions[i].normalized * interestWeights[i];
                var second = currentPosition + 2 * directions[i].normalized * interestWeights[i];
                Gizmos.DrawLine(first, second);
            }

            Gizmos.color = Color.red;
            foreach (var point in contactPoints)
            {
                Gizmos.DrawSphere((Vector2)transform.position + point, 0.5f);
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, resultDirection * 2);
        }

        private static readonly Vector2[] directions =
        {
            new(1, 1),
            new(-1, 1),
            new(1, -1),
            new(-1, -1),
            Vector2.down,
            Vector2.up,
            Vector2.right,
            Vector2.left,
        };
    }
}