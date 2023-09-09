using System.Collections.Generic;
using System.Linq;
using Source.Common.AI.Sensors.Interfaces;
using Source.Common.Extensions;
using UnityEngine;

namespace Source.Common.AI.Sensors
{
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class MultiTargetSensor : MonoBehaviour, ISensor
    {
        [SerializeField] private LayerMask scanningMask;

        private CircleCollider2D sensorCollider;
        private readonly List<Collider2D> detectedTargets = new();

        public float Range => sensorCollider.radius;
        public Collider2D Target => detectedTargets.FirstOrDefault();
        public IEnumerable<Collider2D> Targets => detectedTargets;

        private void Start()
        {
            sensorCollider = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (scanningMask.Includes(other.gameObject.layer))
                detectedTargets.Add(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (detectedTargets.Contains(other))
                detectedTargets.Remove(other);
        }
    }
}