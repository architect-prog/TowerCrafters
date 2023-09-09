using System;
using Source.Common.AI.StateMachine.Interfaces;

namespace Source.Common.AI.Contracts
{
    public class Transition
    {
        public int Weight { get; }
        public IState To { get; }
        public Func<bool> Condition { get; }

        public Transition(IState to, Func<bool> condition, int weight = 0)
        {
            To = to;
            Condition = condition;
            Weight = weight;
        }
    }
}