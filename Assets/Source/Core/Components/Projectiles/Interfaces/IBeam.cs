using UnityEngine;

namespace Source.Core.Components.Projectiles.Interfaces
{
    public interface IBeam
    {
        void SetTarget(GameObject target);

        void Enable();

        void Disable();
    }
}