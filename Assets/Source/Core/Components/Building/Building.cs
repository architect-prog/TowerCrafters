using Source.Common.Utils;
using Source.Core.Constants;
using Source.Kernel.Data;
using UnityEngine;

namespace Source.Core.Components.Building
{
    public class Building : MonoBehaviour
    {
        private readonly ILogger logger = ApplicationRoot.Instance.Logger;

        [SerializeField] private BuildingData data;

        public Vector2 Position => transform.position;
        public Vector2 RootPosition => Position - data.RootOffset;

        public BuildingData Data => data;

        private void OnDrawGizmos()
        {
            if (data is null)
            {
                var message = string.Format(LoggingConstants.NotInitialized, nameof(BuildingData), name);
                logger.LogWarning(nameof(Building), message);
                return;
            }

            Gizmos.color = Colors.SemitransparentGreen;

            for (var i = 0; i < data.Size.x; i++)
            {
                for (var j = 0; j < data.Size.y; j++)
                {
                    var startPosition = RootPosition + new Vector2(i, j);
                    GizmosUtils.DrawRect(startPosition, Vector2.one);
                }
            }

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(RootPosition, 0.2f);
        }
    }
}