using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Source.Common.AI.Contracts;
using Source.Common.AI.StateMachine.Interfaces;
using Source.Common.Extensions;
using UnityEngine;

namespace Source.Common.AI.StateMachine
{
    public sealed class StateMachine : IStateMachine
    {
        private readonly MonoBehaviour owner;
        private readonly List<Transition> anyTransitions  = new();
        private readonly Dictionary<Type, List<Transition>> transitions = new();

        private bool working;
        private Coroutine workingCoroutine;
        private IState currentState;
        private List<Transition> currentStateTransitions = new();

        public StateMachine(MonoBehaviour owner)
        {
            this.owner = owner;
        }

        public StateMachine AddTransition(IState from, IState to, Func<bool> condition, int weight = 0)
        {
            var fromStateType = from.GetType();
            var transition = new Transition(to, condition, weight);

            if (!transitions.ContainsKey(fromStateType))
            {
                transitions.Add(fromStateType, new List<Transition>());
            }

            transitions[fromStateType].Add(transition);

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
            currentStateTransitions = GetStateTransitions(state);
        }

        public void Start()
        {
            working = true;
            workingCoroutine ??= owner.StartCoroutine(Update());
        }

        public void Stop()
        {
            working = false;
            owner.StopCoroutine(workingCoroutine);
            workingCoroutine = null;
        }

        private IEnumerator Update()
        {
            while (working)
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
            if (!currentStateTransitions.Any())
                return null;

            var result = currentStateTransitions
                .Where(x => x.Condition())
                .MaxBy(x => x.Weight);

            return result?.To;
        }

        private List<Transition> GetStateTransitions(IState state)
        {
            var stateType = state.GetType();
            var result = new List<Transition>(anyTransitions);

            if (transitions.TryGetValue(stateType, out var stateTransitions))
                result.AddRange(stateTransitions);

            return result;
        }
    }
}