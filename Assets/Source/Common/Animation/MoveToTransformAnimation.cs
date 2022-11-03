using System.Collections;
using Source.Common.Animation.Interfaces;
using UnityEngine;

namespace Source.Common.Animation
{
    public class MoveToTransformAnimation : IAnimationEffect
    {
        private readonly Transform animationTarget;
        private readonly Transform destination;
        private readonly float moveTime;

        private Vector3? startPosition;
        private Vector3 destinationPosition;
        private float progress;

        public MoveToTransformAnimation(
            Transform animationTarget,
            Transform destination,
            float moveTime)
        {
            this.animationTarget = animationTarget;
            this.destination = destination;
            this.moveTime = moveTime;
        }

        public IEnumerator Execute()
        {
            startPosition ??= animationTarget.position;

            while (progress / moveTime < 1)
            {
                if (!animationTarget)
                    yield break;

                destinationPosition = destination ? destination.position : destinationPosition;

                animationTarget.position = Vector3
                    .Lerp(startPosition.Value, destinationPosition, Mathf.Clamp01(progress / moveTime));

                progress += Time.deltaTime;
                yield return null;
            }
        }
    }
}