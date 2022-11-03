using System.Linq;
using Source.Common.Utils;
using Source.Core.Components.Projectiles;
using Source.Core.Components.Towers.Abstractions;
using Source.Core.Components.Towers.Interfaces;
using Source.Core.Components.Units.Enemies;
using Source.Core.Constants;
using UnityEngine;

namespace Source.Core.Components.Towers
{
    public class ProjectileTower : BaseTower<Enemy>
    {
        [SerializeField] private Projectile projectilePrefab;

        private ITargetSelector targetSelector;
        private Enemy selectedEnemy;
        private RepeatingTimer timer;

        private void Start()
        {
            if (projectilePrefab is null)
            {
                var message = string.Format(LoggingConstants.NotInitialized, nameof(Projectile), name);
                logger.LogWarning(nameof(ProjectileTower), message);
            }
        }

        private void OnEnable()
        {
            targetSelector = new SingleNearestTargetSelector(this);
            timer = new RepeatingTimer(this)
                .WithFrequency(Data.AttackRate)
                .WithAction(() => Attack())
                .InvokeImmediately(true);
        }

        private void OnDisable()
        {
            targetSelector.Dispose();
            timer.Dispose();
        }

        public override void Attack()
        {
            if (projectilePrefab is null)
                return;

            if (selectedEnemy is null)
                return;
            
            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.MoveTo(selectedEnemy.transform, Data.Damage);
        }

        public override void UpdateTargets(params Enemy[] targets)
        {
            if (!targets.Any())
            {
                TargetOver();
                return;
            }

            var target = targetSelector.GetTarget(targets, selectedEnemy);
            TargetAppear(target);
        }

        private void TargetAppear(Enemy target)
        {
            selectedEnemy = target;
            timer.Start();
        }

        private void TargetOver()
        {
            selectedEnemy = null;
            timer.Stop();
        }
    }
}