using Source.Common.AI.Contracts;
using Source.Common.AI.Sensors;
using UnityEngine;

namespace Source.Common.AI.ContextSteering
{
    public class SingleTargetSeekingBehaviour : BaseSteeringBehaviour
    {
        [SerializeField] private SingleTargetSensor targetsSensor;

        public override void UpdateWeights(Vector2 position, Vector2[] directions, ContextSteeringWeights weights)
        {
            var target = targetsSensor.Target;
            if (target is null)
                return;

            var directionToTarget = target.ClosestPoint(position) - position;
            var normalizedDirection = directionToTarget.normalized;
            for (var i = 0; i < directions.Length; i++)
            {
                var result = Vector2.Dot(normalizedDirection, directions[i]);
                if (result > weights.Interest[i] && result > 0)
                {
                    weights.Interest[i] = result;
                }
            }
        }
    }
}