using Source.Common.AI.Sensors;
using Source.Common.AI.StateMachine;
using Source.Common.AI.StateMachine.Interfaces;
using Source.Common.DI;
using Source.Core.Components.Units.States;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Units
{
    public class EnemyAIBrain : MonoBehaviour
    {
        [SerializeField] private SingleTargetSensor targetSensor;

        private IHealthComponent health;
        private IStateMachine stateMachine;
        private MoveState moveState;
        private AttackState attackState;
        private DieState dieState;
        private WoundState woundState;

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
                .AddTransition(attackState, moveState, () => targetSensor.Target is null)
                .AddTransition(woundState, moveState, () => targetSensor.Target is null)
                .AddAnyTransition(attackState, () => targetSensor.Target is not null);

            stateMachine.SetState(moveState);
            stateMachine.Start();

            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void DiedHandler() => stateMachine.SetState(dieState);

        private void DamageTakenHandler() => stateMachine.SetState(woundState);

        private void SubscribeToEvents()
        {
            health.damageTaken += DamageTakenHandler;
            health.died += DiedHandler;
        }

        private void UnsubscribeFromEvents()
        {
            health.damageTaken -= DamageTakenHandler;
            health.died -= DiedHandler;
        }
    }
}