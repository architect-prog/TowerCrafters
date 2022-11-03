using Source.Kernel.Contracts;
using UnityEngine;

namespace Source.Core.Components.Projectiles.Interfaces
{
    public interface IProjectile
    {
        void MoveTo(Transform target, DamageAmount damageAmount);
    }
}