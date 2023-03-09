using Source.Kernel.Data;

namespace Source.Core.Components.Abilities.Interfaces
{
    public interface IAbility
    {
        AbilityData Data { get; }
        float Cooldown { get; }

        void Execute();
    }
}