using System.Collections;
using Source.Common.AI.Sensors.Interfaces;
using Source.Common.AI.StateMachine.Interfaces;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Units.States
{
    public class AttackState : IState
    {
        private readonly AnimatorAdapter animatorAdapter;
        private readonly IAttackComponent attackComponent;
        private readonly ISensor targetProvider;
        private readonly IRotatingComponent rotatingComponent;

        public AttackState(
            AnimatorAdapter animatorAdapter,
            IAttackComponent attackComponent,
            ISensor targetProvider,
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
            rotatingComponent.Rotate(targetProvider.Target.gameObject);
            yield return new WaitForSeconds(attackComponent.DelayBeforeAttack);
            attackComponent.Attack();
            yield return new WaitForSeconds(attackComponent.AttackFrequency);
        }

        public void Exit()
        {
        }
    }
}