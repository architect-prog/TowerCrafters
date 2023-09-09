using Source.Common.AI.ContextSteering.Interfaces;
using Source.Common.AI.Contracts;
using UnityEngine;

namespace Source.Common.AI.ContextSteering
{
    public abstract class BaseSteeringBehaviour : MonoBehaviour, ISteeringBehaviour
    {
        public abstract void UpdateWeights(Vector2 position, Vector2[] directions, ContextSteeringWeights weights);
    }
}