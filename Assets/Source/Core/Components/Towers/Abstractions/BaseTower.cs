using Source.Core.Constants;
using Source.Kernel.Data;
using UnityEngine;

namespace Source.Core.Components.Towers.Abstractions
{
    public abstract class BaseTower<TTarget> : MonoBehaviour
    {
        protected readonly ILogger logger = ApplicationRoot.Instance.Logger;

        [SerializeField] private TowerData data;

        public TowerData Data => data;

        public abstract void Attack();
        public abstract void UpdateTargets(params TTarget[] targets);

        private void OnValidate()
        {
            if (data is null)
            {
                var message = string.Format(LoggingConstants.NotInitialized, nameof(TowerData), name);
                logger.LogWarning(nameof(ProjectileTower), message);
            }
        }
    }
}