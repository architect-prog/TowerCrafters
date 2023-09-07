using System;
using System.Collections.Generic;
using System.Linq;
using Source.Common.AI.Interfaces;
using Source.Common.Extensions;
using UnityEngine;

namespace Source.Common.AI
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class MultiTargetSensor : MonoBehaviour, ITargetProvider
    {
        [SerializeField] private LayerMask scanningMask;

        private readonly List<GameObject> detectedTargets = new();

        public event Action<IEnumerable<GameObject>> targetsChanged;
        public GameObject Target => detectedTargets.FirstOrDefault();
        public IEnumerable<GameObject> Targets => detectedTargets;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (scanningMask.Includes(other.gameObject.layer))
            {
                detectedTargets.Add(other.gameObject);
                targetsChanged?.Invoke(detectedTargets);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (detectedTargets.Contains(other.gameObject))
            {
                detectedTargets.Remove(other.gameObject);
                targetsChanged?.Invoke(detectedTargets);
            }
        }
    }
}