using System;
using Source.Common.DI;
using Source.Common.DI.Extensions;
using Source.Common.DI.Interfaces;
using Source.Core.Components.Loot;
using Source.Core.Components.StatusEffects;
using Source.Core.Components.StatusEffects.Interfaces;
using Source.Core.Components.Units.Common;
using Source.Kernel.Data;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Units.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyData data;

        private IContainer container;
        private EnemyDataProxy enemyDataProxy;
        private IStatusEffectHolder<IStatusEffect> statusEffectHolder;
        private Lazy<IHealthComponent> healthComponent;

        public EnemyDataProxy Data => enemyDataProxy;
        public IHealthComponent HealthComponent => healthComponent.Value;

        private void Awake()
        {
            enemyDataProxy = new EnemyDataProxy(data);
            statusEffectHolder = new StatusEffectHolder(this);
            statusEffectHolder.Enable();

            var obj = gameObject;
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterComponent<Rigidbody2D>(obj);
            containerBuilder.RegisterComponent<ILootBag, LootBag>(obj);
            containerBuilder.RegisterComponent<IAttackComponent, AttackComponent>(obj);
            containerBuilder.RegisterComponent<IDeathComponent, DestroyAtDeathComponent>(obj);
            //containerBuilder.RegisterComponent<ITargetMovingComponent, WaypointMovingComponent>(obj);
            containerBuilder.RegisterComponent<ITargetMovingComponent, PathfinderMovingComponent>(obj);
            containerBuilder.RegisterComponent<IProtectorateAttackComponent, ProtectorateAttackComponent>(obj);
            containerBuilder.RegisterComponent<IHealthComponent, HealthComponent>(obj);
            containerBuilder.RegisterComponent<IRotatingComponent, RotatingComponent>(obj);
            containerBuilder.RegisterComponent<EnemyAIBrain>(obj);
            containerBuilder.RegisterComponent<AnimatorAdapter>(obj);
            containerBuilder.RegisterAsImplementedInterfaces(() => Data);

            container = containerBuilder.Build();
            container.Construct();

            healthComponent = new Lazy<IHealthComponent>(() => container.Resolve<IHealthComponent>());
        }

        public void ApplyEffect(IStatusEffect statusEffect)
        {
            statusEffectHolder.Add(statusEffect);
        }
    }
}