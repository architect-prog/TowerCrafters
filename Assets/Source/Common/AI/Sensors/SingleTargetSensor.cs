using System;
using System.Collections.Generic;
using Source.Common.AI.Sensors.Interfaces;
using Source.Common.Extensions;
using UnityEngine;

namespace Source.Common.AI.Sensors
{
    [RequireComponent(typeof(CircleCollider2D))]
    public sealed class SingleTargetSensor : MonoBehaviour, ISensor
    {
        [SerializeField] private LayerMask scanningMask;
        [SerializeField] private bool forgetOnNewDetected;

        private CircleCollider2D sensorCollider;
        private Collider2D detectedTarget;

        public float Range => sensorCollider.radius;
        public Collider2D Target => detectedTarget;
        public IEnumerable<Collider2D> Targets => detectedTarget
            ? new[] { detectedTarget }
            : Array.Empty<Collider2D>();

        private void Start()
        {
            sensorCollider = GetComponent<CircleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (forgetOnNewDetected || detectedTarget == null)
            {
                if (scanningMask.Includes(other.gameObject.layer))
                    detectedTarget = other;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (detectedTarget is not null)
            {
                if (detectedTarget.gameObject == other.gameObject)
                    detectedTarget = null;
            }
        }
    }
}