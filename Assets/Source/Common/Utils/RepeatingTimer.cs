using System;
using System.Collections;
using UnityEngine;

namespace Source.Common.Utils
{
    public class RepeatingTimer
    {
        private Action action;
        private bool immediately;
        private float timerFrequency;
        private Coroutine coroutine;
        private MonoBehaviour owner;

        private float countdown;
        private bool timerWorks;

        public RepeatingTimer(MonoBehaviour owner)
        {
            this.owner = owner;
        }

        public RepeatingTimer WithFrequency(float frequency)
        {
            timerFrequency = frequency;
            return this;
        }

        public RepeatingTimer WithAction(Action timerActon)
        {
            action = timerActon;
            return this;
        }

        public RepeatingTimer InvokeImmediately(bool startImmediately)
        {
            immediately = startImmediately;
            return this;
        }

        public void Start()
        {
            timerWorks = true;
            coroutine ??= owner.StartCoroutine(Action());
        }

        public void Stop()
        {
            if (coroutine is null)
                return;

            owner.StopCoroutine(coroutine);
            coroutine = null;
            timerWorks = false;
        }

        public void Dispose()
        {
            Stop();
            action = null;
            owner = null;
        }

        private IEnumerator Action()
        {
            if (immediately)
                action?.Invoke();

            countdown = timerFrequency;
            while (timerWorks)
            {
                if (countdown <= 0)
                {
                    action?.Invoke();
                    countdown = timerFrequency;
                }
                else
                {
                    countdown -= Time.deltaTime;
                }

                yield return null;
            }
        }
    }

    public class RepeatingTimer<T>
    {
        private Action<T> action;
        private bool immediately;
        private float timerFrequency;
        private Coroutine coroutine;
        private MonoBehaviour owner;

        private float countdown;
        private bool timerWorks;

        public RepeatingTimer(MonoBehaviour owner)
        {
            this.owner = owner;
        }

        public RepeatingTimer<T> WithFrequency(float frequency)
        {
            timerFrequency = frequency;
            return this;
        }

        public RepeatingTimer<T> WithAction(Action<T> timerActon)
        {
            action = timerActon;
            return this;
        }

        public RepeatingTimer<T> InvokeImmediately(bool startImmediately)
        {
            immediately = startImmediately;
            return this;
        }

        public void Start(T value)
        {
            timerWorks = true;
            coroutine ??= owner.StartCoroutine(Action(value));
        }

        public void Stop()
        {
            if (coroutine is null)
                return;

            owner.StopCoroutine(coroutine);
            coroutine = null;
            timerWorks = false;
        }

        public void Dispose()
        {
            Stop();
            action = null;
            owner = null;
        }

        private IEnumerator Action(T value)
        {
            if (immediately)
                action?.Invoke(value);

            countdown = timerFrequency;
            while (timerWorks)
            {
                if (countdown <= 0)
                {
                    action?.Invoke(value);
                    countdown = timerFrequency;
                }
                else
                {
                    countdown -= Time.deltaTime;
                }

                yield return null;
            }
        }
    }
}