using System.Collections.Generic;
using System.Linq;
using Source.Core.Components.Towers.Abstractions;
using Source.Core.Components.Units.Enemies;
using Source.Core.Constants;
using UnityEngine;

namespace Source.Core.Components.Towers
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(BaseTower<Enemy>))]
    public class TowerTargetProvider : BaseTargetProvider<Enemy>
    {
        private readonly ILogger logger = ApplicationRoot.Instance.Logger;

        private CircleCollider2D scanner;
        private BaseTower<Enemy> tower;

        private void Start()
        {
            tower = GetComponent<BaseTower<Enemy>>();
            scanner = GetComponent<CircleCollider2D>();

            scanner.radius = tower.Data.AttackRange;
        }

        public override void ProvideTargets(IEnumerable<Enemy> targets)
        {
            tower.UpdateTargets(targets.ToArray());
        }

        private void OnDrawGizmos()
        {
            tower = GetComponent<BaseTower<Enemy>>();
            if (tower is null)
            {
                var message = string.Format(LoggingConstants.NotAssigned, nameof(BaseTower<Enemy>), name);
                logger.LogWarning(nameof(ProjectileTower), message);
                return;
            }

            var towerData = tower.Data;
            if (towerData is null)
                return;

            Gizmos.color = Colors.SemitransparentGreen;
            Gizmos.DrawWireSphere(transform.position, towerData.AttackRange);
        }
    }
}