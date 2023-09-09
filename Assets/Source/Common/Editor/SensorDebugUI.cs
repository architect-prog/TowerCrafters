using System;
using System.Linq;
using Source.Common.AI.Sensors.Interfaces;
using UnityEngine;

namespace Source.Common.Editor
{
    [ExecuteAlways]
    [RequireComponent(typeof(ISensor))]
    public class SensorDebugUI : MonoBehaviour
    {
        [SerializeField] private Color areaColor;
        [SerializeField] private Color targetsColor;
        [SerializeField] private float targetRadius;

        private Mesh detectingArea;
        private Collider2D detectingCollider;
        private Bounds detectingColliderBounds;
        private ISensor[] sensors = Array.Empty<ISensor>();

        private void Start()
        {
            sensors = GetComponents<ISensor>();
            detectingCollider = GetComponent<Collider2D>();
        }

        private void OnDrawGizmos()
        {
            if (!enabled)
                return;

            Gizmos.color = targetsColor;
            var targets = sensors.SelectMany(x => x.Targets).ToArray();

            foreach (var target in targets)
            {
                Gizmos.DrawSphere(target.transform.position, targetRadius);
            }

            Gizmos.color = areaColor;
            if (detectingCollider is not null)
            {
                if (detectingColliderBounds != detectingCollider.bounds)
                {
                    detectingColliderBounds = detectingCollider.bounds;
                    detectingArea = detectingCollider.CreateMesh(true, true);
                    detectingArea.RecalculateNormals();
                }

                Gizmos.DrawMesh(detectingArea);
            }
        }
    }
}