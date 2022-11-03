using System.Collections;
using Source.Common.AI.Interfaces;

namespace Source.Core.Components.Units.States
{
    public class WoundState : IState
    {
        private readonly AnimatorAdapter animatorAdapter;

        public WoundState(AnimatorAdapter animatorAdapter)
        {
            this.animatorAdapter = animatorAdapter;
        }

        public void Enter()
        {
            animatorAdapter.Wound();
        }

        public IEnumerator Update()
        {
            yield break;
        }

        public void Exit()
        {
        }
    }
}