using Source.Kernel.Interfaces.Components;
using UnityEngine;

namespace Source.Core.Components.Units.Enemies
{
    public class DestroyAtDeathComponent : MonoBehaviour, IDeathComponent
    {
        public void Die()
        {
            Destroy(transform.root.gameObject);
        }
    }
}