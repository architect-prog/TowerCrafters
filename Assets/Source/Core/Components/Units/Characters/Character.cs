using Source.Common.DI;
using Source.Common.DI.Extensions;
using Source.Common.DI.Interfaces;
using Source.Core.Components.StatusEffects;
using Source.Core.Components.StatusEffects.Interfaces;
using Source.Core.Components.Units.Common;
using Source.Core.Constants;
using Source.Kernel.Data;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Units.Characters
{
    public sealed class Character : MonoBehaviour
    {
        [SerializeField] private CharacterData data;

        private IContainer container;
        private CharacterDataProxy characterDataProxy;
        private IStatusEffectHolder<IStatusEffect> statusEffectHolder;

        public CharacterDataProxy Data => characterDataProxy;

        private void Start()
        {
            characterDataProxy = new CharacterDataProxy(data);
            statusEffectHolder = new StatusEffectHolder(this);
            statusEffectHolder.Enable();

            var obj = gameObject;
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterComponent<Rigidbody2D>(obj);
            containerBuilder.RegisterComponent<IHealthComponent, HealthComponent>(obj);
            containerBuilder.RegisterComponent<IMovingComponent, MovingComponent>(obj);
            containerBuilder.RegisterComponent<IRotatingComponent, RotatingComponent>(obj);
            containerBuilder.RegisterComponent<IItemAttractionComponent, ItemAttractionComponent>(obj);
            containerBuilder.RegisterAsImplementedInterfaces(() => Data);

            container = containerBuilder.Build();
            container.Construct();
        }

        public void ApplyEffect(IStatusEffect statusEffect)
        {
            statusEffectHolder.Add(statusEffect);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Colors.SemitransparentYellow;
            Gizmos.DrawWireSphere(transform.position, data.CollectingRange);
        }
    }
}