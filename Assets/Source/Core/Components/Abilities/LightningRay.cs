using Source.Common.Utils;
using Source.Core.Components.Abilities.Contracts;
using Source.Core.Components.Abilities.Interfaces;
using Source.Core.Components.Projectiles;
using Source.Core.Constants;
using Source.Kernel.Contracts;
using Source.Kernel.Data;
using Source.Kernel.Enums;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Abilities
{
    public class LightningRay : MonoBehaviour, IAbility
    {
        private readonly ILogger logger = ApplicationRoot.Instance.Logger;

        [SerializeField] private Beam beamPrefab;
        [SerializeField] private AbilityData data;
        [SerializeField] private LayerMask layers;
        [SerializeField] private float areaOfEffect;

        private Beam beam;
        private Timer timer;
        private Timer timer1;

        public AbilityData Data => data;
        public float Cooldown { get; set; }

        private void Start()
        {
            if (beamPrefab is null)
            {
                var message = string.Format(LoggingConstants.NotInitialized, nameof(Beam), name);
                logger.LogWarning(nameof(LightningRay), message);
                return;
            }

            beam = Instantiate(beamPrefab, transform, false);
            beam.Disable();

            timer = new Timer(this)
                .WithDelay(0.2f)
                .WithAction(() => beam.Disable());

            timer1 = new Timer(this)
                .WithDelay(data.MaxCooldown)
                .WithAction(() => Cooldown = 0);
        }

        public void Execute(AbilityExecutingData abilityExecutingData)
        {
            if (Cooldown > 0)
                return;

            var castPosition = abilityExecutingData.CastPosition;

            var position = (Vector2)transform.position;
            var distance = Vector2.Distance(position, castPosition);
            var hit = Physics2D.Raycast(position, (castPosition - position).normalized, distance, layers);
            if (hit.collider != null)
            {
                castPosition = hit.point;
            }

            var targets = Physics2D.OverlapCircleAll(castPosition, 2f, layers);
            foreach (var target in targets)
            {
                var targetHealth = target?.GetComponent<IHealthComponent>();
                targetHealth?.ApplyDamage(new DamageAmount(new Damage(2f, DamageType.Lightning)));
            }

            beam.SetTarget(castPosition);
            Cooldown = data.MaxCooldown;
            beam.Enable();
            timer.Start();
            timer1.Start();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, areaOfEffect);
        }
    }
}