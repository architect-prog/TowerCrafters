using Source.Core.Components.Abilities.Contracts;
using Source.Kernel.Data;

namespace Source.Core.Components.Abilities.Interfaces
{
    public interface IAbility
    {
        AbilityData Data { get; }
        float Cooldown { get; set; }

        void Execute(AbilityExecutingData abilityExecutingData);
    }
}