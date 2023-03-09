using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Source.Common.AI.Contracts;
using Source.Common.AI.Interfaces;
using Source.Common.Extensions;
using UnityEngine;

namespace Source.Common.AI
{
    public sealed class StateMachine : IStateMachine
    {
        private readonly MonoBehaviour owner;
        private readonly List<Transition> anyTransitions;
        private readonly Dictionary<Type, List<Transition>> transitions;

        private IState currentState;
        private Transition[] currentStateTransitions;

        private bool isWorking;
        private Coroutine coroutine;

        public StateMachine(MonoBehaviour owner)
        {
            this.owner = owner;
            anyTransitions = new List<Transition>();
            transitions = new Dictionary<Type, List<Transition>>();

            currentStateTransitions = Array.Empty<Transition>();
        }

        public StateMachine AddTransition(IState from, IState to, Func<bool> condition, int weight = 0)
        {
            var fromStateType = from.GetType();
            var transition = new Transition(to, condition, weight);

            if (!transitions.TryGetValue(fromStateType, out var stateTransitions))
            {
                stateTransitions = new List<Transition>();
                transitions[fromStateType] = stateTransitions;
            }

            stateTransitions.Add(transition);
            return this;
        }

        public StateMachine AddAnyTransition(IState to, Func<bool> condition, int weight = 0)
        {
            var transition = new Transition(to, condition, weight);
            anyTransitions.Add(transition);
            return this;
        }

        public void SetState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            currentState?.Exit();
            state.Enter();

            currentState = state;
            currentStateTransitions = GetStateTransitions(state)?.ToArray() ?? Array.Empty<Transition>();
        }

        public void Start()
        {
            isWorking = true;
            coroutine ??= owner.StartCoroutine(Update());
        }

        public void Stop()
        {
            isWorking = false;
            owner.StopCoroutine(coroutine);
            coroutine = null;
        }

        private IEnumerator Update()
        {
            while (isWorking)
            {
                var nextState = GetNextStateOrDefault();
                if (nextState is not null)
                {
                    SetState(nextState);
                }

                yield return owner.StartCoroutine(currentState.Update());
            }
        }

        private IState GetNextStateOrDefault()
        {
            var result = currentStateTransitions.Where(x => x.Condition())
                .MinBy(x => x.Weight);

            return result?.To;
        }

        private IEnumerable<Transition> GetStateTransitions(IState state)
        {
            var stateType = state.GetType();
            if (transitions.TryGetValue(stateType, out var stateTransitions))
            {
                foreach (var transition in stateTransitions)
                {
                    yield return transition;
                }
            }

            foreach (var transition in anyTransitions)
            {
                yield return transition;
            }
        }
    }
}