using UnityEngine;

namespace Source.Kernel.Interfaces.Components
{
    public interface IRotatingComponent
    {
        void Rotate(GameObject target);
        void Rotate(Vector3 direction);
    }
}