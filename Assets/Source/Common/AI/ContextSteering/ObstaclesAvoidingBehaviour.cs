using System.Linq;
using Source.Common.AI.Contracts;
using Source.Common.AI.Sensors;
using UnityEngine;

namespace Source.Common.AI.ContextSteering
{
    public class ObstaclesAvoidingBehaviour : BaseSteeringBehaviour
    {
        [SerializeField] private float agentRadius;
        [SerializeField] private MultiTargetSensor obstaclesSensor;

        public override void UpdateWeights(Vector2 position, Vector2[] directions, ContextSteeringWeights weights)
        {
            var obstacles = obstaclesSensor.Targets.ToArray();
            foreach (var obstacle in obstacles)
            {
                var directionToObstacle = obstacle.ClosestPoint(position) - position;
                var distanceToObstacle = directionToObstacle.magnitude;
                var normalizedDirection = directionToObstacle.normalized;
                var scanningRange = obstaclesSensor.Range;

                var weight = distanceToObstacle > agentRadius
                    ? (scanningRange - distanceToObstacle) / scanningRange
                    : 1;

                for (var i = 0; i < directions.Length; i++)
                {
                    var result = Vector2.Dot(normalizedDirection, directions[i]);
                    var weightedResult = result * weight;

                    if (weightedResult > weights.Danger[i])
                        weights.Danger[i] = weightedResult;
                }
            }
        }
    }
}