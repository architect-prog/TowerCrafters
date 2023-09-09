using System.Linq;
using Source.Common.AI.Contracts;
using Source.Common.AI.Sensors;
using Source.Common.Extensions;
using UnityEngine;

namespace Source.Common.AI.ContextSteering
{
    public class TargetsSeekingBehaviour : BaseSteeringBehaviour
    {
        [SerializeField] private MultiTargetDetectSensor targetsSensor;

        private Vector2 targetLastPosition;

        public override void UpdateWeights(Vector2 position, Vector2[] directions, ContextSteeringWeights weights)
        {
            var targets = targetsSensor.Targets.ToArray();
            var nearestTarget = targetsSensor.GetTargetsInSight().GetNearestFor(position);

            targetLastPosition = nearestTarget is null
                ? targetLastPosition
                : nearestTarget.transform.position;

            if (targetLastPosition == Vector2.zero)
                return;

            var directionToTarget = targetLastPosition - position;
            var distanceToTarget = directionToTarget.magnitude;
            var normalizedDirection = directionToTarget.normalized;
            var scanningRange = targetsSensor.Range;

            for (var i = 0; i < directions.Length; i++)
            {
                var result = Vector2.Dot(normalizedDirection, directions[i]);

                if (result > weights.Interest[i])
                    weights.Interest[i] = result;
            }
        }

        private void OnDrawGizmos()
        {
            if (targetLastPosition == Vector2.zero)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(targetLastPosition, 0.3f);
        }
    }
}