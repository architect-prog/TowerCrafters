using Source.Common.AI;
using Source.Common.AI.Interfaces;
using Source.Common.DI;
using Source.Core.Components.Units.States;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Units
{
    public class EnemyAIBrain : MonoBehaviour
    {
        [SerializeField] private SingleTargetSensor targetSensor;

        private IStateMachine stateMachine;
        private MoveState moveState;
        private AttackState attackState;
        private DieState dieState;
        private WoundState woundState;

        private IHealthComponent health;
        private GameObject attackTarget;

        [Construct]
        public void Construct(
            ILootBag lootBag,
            IDeathComponent deathComponent,
            IAttackComponent attackComponent,
            IHealthComponent healthComponent,
            AnimatorAdapter animatorAdapter,
            ITargetMovingComponent targetMovingComponent,
            IRotatingComponent rotatingComponent)
        {
            health = healthComponent;

            moveState = new MoveState(animatorAdapter, targetMovingComponent);
            attackState = new AttackState(animatorAdapter, attackComponent, targetSensor, rotatingComponent);
            dieState = new DieState(animatorAdapter, deathComponent, lootBag);
            woundState = new WoundState(animatorAdapter);

            stateMachine = new StateMachine(this)
                .AddTransition(attackState, moveState, () => !attackTarget)
                .AddTransition(woundState, moveState, () => !attackTarget)
                .AddAnyTransition(attackState, () => attackTarget);

            stateMachine.SetState(moveState);
            stateMachine.Start();
        }

        private void OnEnable()
        {
            targetSensor.targetChanged += TargetChangeHandler;
            health.damageTaken += DamageTakenHandler;
            health.died += DiedHandler;
        }

        private void OnDisable()
        {
            targetSensor.targetChanged -= TargetChangeHandler;
            health.damageTaken -= DamageTakenHandler;
            health.died -= DiedHandler;
        }

        private void TargetChangeHandler(GameObject detectedTarget) => attackTarget = detectedTarget;

        private void DamageTakenHandler() => stateMachine.SetState(woundState);

        private void DiedHandler() => stateMachine.SetState(dieState);
    }
}