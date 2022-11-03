using Source.Kernel.Enums;
using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Units.Common
{
    public class RotatingComponent : MonoBehaviour, IRotatingComponent
    {
        [SerializeField] private FaceDirection faceDirection;

        public void Rotate(GameObject target)
        {
            if (!target)
                return;

            var direction = target.transform.position - transform.position;
            Flip(direction);
        }

        public void Rotate(Vector3 direction)
        {
            if (direction.x == 0)
                return;

            Flip(direction);
        }

        private void Flip(Vector2 direction)
        {
            switch (faceDirection)
            {
                case FaceDirection.Right:
                    transform.right = new Vector2(direction.x, 0);
                    return;
                case FaceDirection.Left:
                    transform.right = new Vector2(-direction.x, 0);
                    return;
            }
        }
    }
}