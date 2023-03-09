using System;
using UnityEngine;

namespace Source.Kernel.Entities
{
    [Serializable]
    public struct AbilityEntity
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private float maxCooldown;

        public Sprite Icon
        {
            readonly get => icon;
            set => icon = value;
        }

        public string Name
        {
            readonly get => name;
            set => name = value;
        }

        public string Description
        {
            readonly get => description;
            set => description = value;
        }

        public float MaxCooldown
        {
            readonly get => maxCooldown;
            set => maxCooldown = value;
        }
    }
}