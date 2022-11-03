using System;
using Source.Kernel.Enums;
using UnityEngine;

namespace Source.Kernel.Contracts
{
    [Serializable]
    public struct Resist
    {
        [SerializeField] [Range(0, 1)] private float resistPercent;
        [SerializeField] private DamageType type;

        public float ResistPercent => resistPercent;
        public DamageType Type => type;

        public Resist(float resistPercent, DamageType type)
        {
            this.resistPercent = Mathf.Clamp01(resistPercent);
            this.type = type;
        }
    }
}