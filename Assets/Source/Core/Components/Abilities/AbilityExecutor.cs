using Source.Common.Messaging;
using Source.Core.Components.Abilities.Interfaces;
using Source.Core.Constants;
using UnityEngine;

namespace Source.Core.Components.Abilities
{
    public class AbilityExecutor : MonoBehaviour
    {
        private void OnEnable()
        {
            MessageBus<IAbility>.AddListener(Messaging.Events.AbilityExecuted, Execute);
        }

        private void OnDisable()
        {
            MessageBus<IAbility>.RemoveListener(Messaging.Events.AbilityExecuted, Execute);
        }

        private void Execute(IAbility ability)
        {
            ability.Execute();
        }
    }
}