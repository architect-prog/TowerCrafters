using System;
using System.Collections.Generic;
using Source.Common.AI.Interfaces;
using Source.Common.Extensions;
using UnityEngine;

namespace Source.Common.AI
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class SingleTargetSensor : MonoBehaviour, ITargetProvider
    {
        [SerializeField] private LayerMask scanningMask;
        [SerializeField] private bool forgetOnNewDetected;

        private GameObject detectedTarget;

        public event Action<GameObject> targetChanged;
        public GameObject Target => detectedTarget;
        public IEnumerable<GameObject> Targets => new[] { detectedTarget };

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (forgetOnNewDetected || detectedTarget == null)
            {
                if (scanningMask.Includes(other.gameObject.layer))
                    UpdateTarget(other.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (detectedTarget == other.gameObject)
                UpdateTarget(null);
        }

        private void UpdateTarget(GameObject target)
        {
            detectedTarget = target;
            targetChanged?.Invoke(detectedTarget);
        }
    }
}