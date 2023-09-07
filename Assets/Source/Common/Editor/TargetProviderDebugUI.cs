using System;
using System.Linq;
using Source.Common.AI.Interfaces;
using UnityEngine;

namespace Source.Common.Editor
{
    [ExecuteAlways]
    [RequireComponent(typeof(ITargetProvider))]
    public class TargetProviderDebugUI : MonoBehaviour
    {
        [SerializeField] private Color areaColor;
        [SerializeField] private Color targetsColor;
        [SerializeField] private float targetRadius;

        private Mesh detectingArea;
        private Collider2D detectingCollider;
        private Bounds detectingColliderBounds;
        private ITargetProvider[] targetProviders = Array.Empty<ITargetProvider>();

        private void Start()
        {
            targetProviders = GetComponents<ITargetProvider>();
            detectingCollider = GetComponent<Collider2D>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = targetsColor;
            var targets = targetProviders.SelectMany(x => x.Targets).ToArray();

            foreach (var target in targets)
            {
                Gizmos.DrawSphere(target.transform.position, targetRadius);
            }

            if (detectingCollider is not null)
            {
                if (detectingColliderBounds != detectingCollider.bounds)
                {
                    detectingColliderBounds = detectingCollider.bounds;
                    detectingArea = detectingCollider.CreateMesh(true, true);
                    detectingArea.RecalculateNormals();
                }

                Gizmos.color = areaColor;
                Gizmos.DrawMesh(detectingArea);
            }
        }
    }
}