using System.Collections;
using Source.Common.Animation.Interfaces;
using UnityEngine;

namespace Source.Common.Animation
{
    public class MoveToTransformBySpeedAnimation : IAnimationEffect
    {
        private readonly Transform animationTarget;
        private readonly Transform destination;
        private readonly float speed;
        private readonly float enoughDistance;

        private Vector3 destinationPosition;

        public MoveToTransformBySpeedAnimation(
            Transform animationTarget,
            Transform destination,
            float speed,
            float enoughDistance = 0.1f)
        {
            this.animationTarget = animationTarget;
            this.destination = destination;
            this.speed = speed;
            this.enoughDistance = enoughDistance;
        }

        public IEnumerator Execute()
        {
            do
            {
                if (!animationTarget)
                    yield break;

                destinationPosition = destination ? destination.position : destinationPosition;

                animationTarget.position = Vector2
                    .MoveTowards(animationTarget.position, destinationPosition, speed * Time.deltaTime);

                yield return null;
            } while (Vector2.Distance(animationTarget.position, destinationPosition) >= enoughDistance);
        }
    }
}