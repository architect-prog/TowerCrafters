using Source.Core.Components.Abilities.Contracts;
using Source.Core.Components.Abilities.Interfaces;
using UnityEngine;

namespace Source.Core.Components.Abilities
{
    public class AbilityExecutor : MonoBehaviour, IAbilityExecutor
    {
        public void Execute(IAbility ability, AbilityExecutingData abilityExecutingData)
        {
            ability.Execute(abilityExecutingData);
        }
    }
}