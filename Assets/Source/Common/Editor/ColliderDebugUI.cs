using UnityEngine;

namespace Source.Common.Editor
{
    [ExecuteAlways]
    public class ColliderDebugUI : MonoBehaviour
    {
        [SerializeField] private Color areaColor;

        private Mesh detectingArea;
        private Collider2D detectingCollider;
        private Bounds detectingColliderBounds;

        private void Start()
        {
            detectingCollider = GetComponent<Collider2D>();
        }

        private void OnDrawGizmos()
        {
            if (!enabled)
                return;

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