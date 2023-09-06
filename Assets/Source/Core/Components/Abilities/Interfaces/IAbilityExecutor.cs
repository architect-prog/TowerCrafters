using Source.Core.Components.Abilities.Contracts;

namespace Source.Core.Components.Abilities.Interfaces
{
    public interface IAbilityExecutor
    {
        void Execute(IAbility ability, AbilityExecutingData abilityExecutingData);
    }
}