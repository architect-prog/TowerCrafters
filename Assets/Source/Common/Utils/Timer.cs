using System;
using System.Collections;
using UnityEngine;

namespace Source.Common.Utils
{
    public class Timer : IDisposable
    {
        private float delay;
        private Action action;
        private Coroutine coroutine;
        private MonoBehaviour owner;

        public Timer(MonoBehaviour owner)
        {
            this.owner = owner;
        }

        public Timer WithDelay(float timerDelay)
        {
            delay = timerDelay;
            return this;
        }

        public Timer WithAction(Action timerActon)
        {
            action = timerActon;
            return this;
        }

        public void Start()
        {
            coroutine ??= owner.StartCoroutine(Action());
        }

        public void Stop()
        {
            if (coroutine is null)
                return;

            owner.StopCoroutine(coroutine);
            coroutine = null;
        }

        public void Dispose()
        {
            Stop();
            owner = null;
            action = null;
        }

        private IEnumerator Action()
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
            Stop();
        }
    }

    public class Timer<T> : IDisposable
    {
        private float delay;
        private Action<T> action;
        private Coroutine coroutine;
        private MonoBehaviour owner;

        public Timer(MonoBehaviour owner)
        {
            this.owner = owner;
        }

        public Timer<T> WithDelay(float timerDelay)
        {
            delay = timerDelay;
            return this;
        }

        public Timer<T> WithAction(Action<T> timerActon)
        {
            action = timerActon;
            return this;
        }

        public void Start(T value)
        {
            coroutine ??= owner.StartCoroutine(Action(value));
        }

        public void Stop()
        {
            if (coroutine is null)
                return;

            owner.StopCoroutine(coroutine);
            coroutine = null;
        }

        public void Dispose()
        {
            Stop();
            owner = null;
            action = null;
        }

        private IEnumerator Action(T value)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke(value);
        }
    }
}