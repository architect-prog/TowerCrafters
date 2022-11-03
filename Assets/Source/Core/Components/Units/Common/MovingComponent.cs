using Source.Common.DI;
using Source.Kernel.Interfaces.Components;
using Source.Kernel.Interfaces.Providers;
using UnityEngine;

namespace Source.Core.Components.Units.Common
{
    public class MovingComponent : MonoBehaviour, IMovingComponent
    {
        private Rigidbody2D movingRigidbody;
        private ISpeedProvider speed;

        [Construct]
        public virtual void Construct(ISpeedProvider speedProvider, Rigidbody2D unitRigidbody)
        {
            movingRigidbody = unitRigidbody;
            speed = speedProvider;
        }

        public void Move(Vector2 direction)
        {
            var movementTarget = direction.normalized * speed.TotalSpeed;
            var currentPosition = movingRigidbody.position;
            movingRigidbody.MovePosition(currentPosition + movementTarget * Time.deltaTime);
        }
    }
}