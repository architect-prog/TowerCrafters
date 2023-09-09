using System.Collections;
using Source.Common.AI.StateMachine.Interfaces;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Units.States
{
    public class MoveState : IState
    {
        private readonly AnimatorAdapter animatorAdapter;
        private readonly ITargetMovingComponent movingComponent;
        private readonly IRotatingComponent rotatingComponent;
        private readonly YieldInstruction yieldInstruction;

        public MoveState(
            AnimatorAdapter animatorAdapter,
            ITargetMovingComponent movingComponent)
        {
            this.animatorAdapter = animatorAdapter;
            this.movingComponent = movingComponent;

            yieldInstruction = new WaitForFixedUpdate();
        }

        public void Enter()
        {
            animatorAdapter.Walk();
        }

        public IEnumerator Update()
        {
            movingComponent.Move();
            yield return yieldInstruction;
        }

        public void Exit()
        {
            animatorAdapter.Idle();
        }
    }
}