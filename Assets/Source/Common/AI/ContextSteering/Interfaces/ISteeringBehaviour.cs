using Source.Common.AI.Contracts;
using UnityEngine;

namespace Source.Common.AI.ContextSteering.Interfaces
{
    public interface ISteeringBehaviour
    {
        void UpdateWeights(Vector2 position, Vector2[] directions, ContextSteeringWeights weights);
    }
}