using System.Collections;
using Source.Common.AI.Interfaces;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Units.States
{
    public class AttackState : IState
    {
        private readonly AnimatorAdapter animatorAdapter;
        private readonly IAttackComponent attackComponent;
        private readonly ITargetProvider targetProvider;
        private readonly IRotatingComponent rotatingComponent;

        public AttackState(
            AnimatorAdapter animatorAdapter,
            IAttackComponent attackComponent,
            ITargetProvider targetProvider,
            IRotatingComponent rotatingComponent)
        {
            this.animatorAdapter = animatorAdapter;
            this.attackComponent = attackComponent;
            this.targetProvider = targetProvider;
            this.rotatingComponent = rotatingComponent;
        }

        public void Enter()
        {
        }

        public IEnumerator Update()
        {
            animatorAdapter.Attack();
            rotatingComponent.Rotate(targetProvider.Target);
            yield return new WaitForSeconds(attackComponent.DelayBeforeAttack);
            attackComponent.Attack();
            yield return new WaitForSeconds(attackComponent.AttackFrequency);
        }

        public void Exit()
        {
        }
    }
}