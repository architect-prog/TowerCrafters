using System;
using Source.Common.AI.Interfaces;
using Source.Common.Extensions;
using UnityEngine;

namespace Source.Common.AI
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class SingleTargetSensor : MonoBehaviour, ITargetProvider
    {
        [SerializeField] private LayerMask scanningMask;

        private GameObject detectedTarget;

        public event Action<GameObject> targetChanged;

        public GameObject Target => detectedTarget;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (scanningMask.Includes(other.gameObject.layer))
            {
                detectedTarget = other.gameObject;
                targetChanged?.Invoke(detectedTarget);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (scanningMask.Includes(other.gameObject.layer))
            {
                if (detectedTarget == other.gameObject)
                {
                    detectedTarget = null;
                    targetChanged?.Invoke(detectedTarget);
                }
            }
        }
    }
}