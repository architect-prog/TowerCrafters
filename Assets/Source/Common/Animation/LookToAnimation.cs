using System.Collections;
using Source.Common.Animation.Interfaces;
using UnityEngine;

namespace Source.Common.Animation
{
    public class LookToAnimation : IAnimationEffect
    {
        private readonly Transform animationTarget;
        private readonly Transform lookTo;

        private Vector3? startPosition;
        private Vector3 destinationPosition;

        public LookToAnimation(
            Transform animationTarget,
            Transform lookTo)
        {
            this.animationTarget = animationTarget;
            this.lookTo = lookTo;
        }

        public IEnumerator Execute()
        {
            startPosition ??= animationTarget.position;

            while (true)
            {
                if (!animationTarget)
                    yield break;

                if (!lookTo)
                    yield break;

                var lookDirection = lookTo.position - animationTarget.position;
                animationTarget.up = lookDirection;

                yield return null;
            }
        }
    }
}