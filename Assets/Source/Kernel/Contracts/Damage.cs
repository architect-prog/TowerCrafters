using System;
using Source.Kernel.Enums;
using UnityEngine;

namespace Source.Kernel.Contracts
{
    [Serializable]
    public struct Damage
    {
        [SerializeField] private float amount;
        [SerializeField] private DamageType type;

        public float Amount => amount;
        public DamageType Type => type;

        public Damage(float amount, DamageType type)
        {
            this.amount = amount;
            this.type = type;
        }
    }
}