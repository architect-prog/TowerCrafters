using System.Collections.Generic;
using Source.Common.Messaging;
using Source.Core.Components.Abilities.Interfaces;
using Source.Core.Constants;
using UnityEngine;

namespace Source.Core.Components.Abilities
{
    public class AbilityHolder : MonoBehaviour
    {
        private Dictionary<IAbility, IAbilityExecutingDataProvider> abilitiesData;
        [SerializeField] private IAbility[] abilities;

        private void OnEnable()
        {
            MessageBus<IAbility[]>.AddListener(Messaging.Events.AbilitiesInitialized, Initialize);
        }

        private void OnDisable()
        {
            MessageBus<IAbility[]>.RemoveListener(Messaging.Events.AbilitiesInitialized, Initialize);
        }

        private void Initialize(IAbility[] abilities)
        {

        }
    }
}