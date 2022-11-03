using System.Collections;
using Source.Common.Animation.Interfaces;
using UnityEngine;

namespace Source.Common.Animation
{
    public class MoveToPositionAnimation : IAnimationEffect
    {
        private readonly Transform animationTarget;
        private readonly Vector3 destination;
        private readonly float moveTime;

        private Vector3? startPosition;
        private float progress;

        public MoveToPositionAnimation(
            Transform animationTarget,
            Vector3 destination,
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

                animationTarget.position = Vector3
                    .Lerp(startPosition.Value, destination, Mathf.Clamp01(progress / moveTime));

                progress += Time.deltaTime;
                yield return null;
            }
        }
    }
}