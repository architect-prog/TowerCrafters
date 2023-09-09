using System;

namespace Source.Common.AI.StateMachine.Interfaces
{
    public interface IStateMachine
    {
        StateMachine AddAnyTransition(IState to, Func<bool> condition, int weight = 0);
        StateMachine AddTransition(IState from, IState to, Func<bool> condition, int weight = 0);
        void Start();
        void Stop();
        void SetState(IState state);
    }
}