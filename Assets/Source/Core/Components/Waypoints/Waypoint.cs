using UnityEngine;

namespace Source.Core.Components.Waypoints
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private Color color;
        [SerializeField] private float radius;

        public Vector3 PointPosition => transform.position;

        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(PointPosition, radius);
        }
    }
}