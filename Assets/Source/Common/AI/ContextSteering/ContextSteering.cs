using System;
using System.Linq;
using Source.Common.AI.Contracts;
using Source.Common.Constants;
using Source.Common.Utils;
using UnityEngine;

namespace Source.Common.AI.ContextSteering
{
    public class ContextSteering : MonoBehaviour
    {
        [SerializeField] [Range(1, 100)] private int numOfDirections;
        [SerializeField] private BaseSteeringBehaviour[] steeringBehaviours;

        private ContextSteeringWeights weights;
        private Vector2[] directions = Array.Empty<Vector2>();

        private void Start()
        {
            directions = GetDirections(numOfDirections);
            weights = new ContextSteeringWeights(numOfDirections);
        }

        public Vector2 MakeDecision(Vector2 currentPosition)
        {
            weights.Clear();

            foreach (var behaviour in steeringBehaviours)
                behaviour.UpdateWeights(currentPosition, directions, weights);

            var outputDirection = Vector2.zero;
            for (var i = 0; i < numOfDirections; i++)
            {
                var result = Mathf.Clamp01(weights.Interest[i] - weights.Danger[i]);
                weights.Result[i] = result;

                outputDirection += directions[i] * result;
            }

            return outputDirection.normalized;
        }

        private static Vector2[] GetDirections(int directionsCount)
        {
            if (directionsCount <= 0)
                return Array.Empty<Vector2>();

            var centerPosition = Vector2.zero;
            var angleBetweenDirections = MathConstants.CircleAngle / directionsCount;

            var result = new Vector2[directionsCount];
            for (var i = 0; i < directionsCount; i++)
            {
                var currentAngle = i * angleBetweenDirections * Mathf.Deg2Rad;
                result[i] = new Vector2(Mathf.Sin(currentAngle), Mathf.Cos(currentAngle));
            }

            return result;
        }

        private void OnDrawGizmos()
        {
            var currentPosition = transform.position;
            var drawingDirections = GetDirections(numOfDirections);
            var drawWeightedLines = new Action<float[], Color>((directionWeights, color) =>
            {
                Gizmos.color = color;
                for (var i = 0; i < numOfDirections; i++)
                {
                    var weightValue = directionWeights.Any() ? directionWeights[i] * 5 : 1;
                    GizmosUtils.DrawRay(currentPosition, drawingDirections[i], weightValue);
                }
            });

            var danderWeights = weights?.Danger ?? Array.Empty<float>();
            drawWeightedLines(danderWeights, Color.red);

            var interestWeights = weights?.Interest ?? Array.Empty<float>();
            drawWeightedLines(interestWeights, Color.green);
        }
    }
}