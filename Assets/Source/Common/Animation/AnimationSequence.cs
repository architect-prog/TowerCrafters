using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Source.Common.Animation.Interfaces;
using UnityEngine;

namespace Source.Common.Animation
{
    public class AnimationSequence : IAnimationSequence
    {
        private readonly List<IAnimationEffect> animations = new();
        private readonly List<Coroutine> childCoroutines = new();
        private Coroutine rootCoroutine;

        private MonoBehaviour owner;
        private bool isStarted;
        private bool isDisposed;

        public AnimationSequence(MonoBehaviour owner)
        {
            this.owner = owner;
        }

        public static IAnimationSequence Create(MonoBehaviour sequenceOwner)
        {
            var result = new AnimationSequence(sequenceOwner);
            return result;
        }

        public IAnimationSequence AddAnimation(IAnimationEffect effect)
        {
            if (isStarted || isDisposed)
                return this;

            animations.Add(effect);
            return this;
        }

        public void Stop()
        {
            if (rootCoroutine is null || isDisposed)
                return;

            owner.StopCoroutine(rootCoroutine);
            rootCoroutine = null;

            foreach (var coroutine in childCoroutines)
            {
                owner.StopCoroutine(coroutine);
            }
            childCoroutines.Clear();
        }

        public void Execute()
        {
            if (isDisposed)
                return;

            if (!animations.Any())
                return;

            rootCoroutine ??= owner.StartCoroutine(ExecuteEffects());
            isStarted = true;
        }

        public void Dispose()
        {
            Stop();
            owner = null;
            isDisposed = true;
        }

        private IEnumerator ExecuteEffects()
        {
            foreach (var animation in animations)
            {
                var coroutine = owner.StartCoroutine(animation.Execute());
                if (coroutine != null)
                    childCoroutines.Add(coroutine);

                yield return coroutine;
            }

            Dispose();
        }
    }
}