namespace Source.Core.Components.StatusEffects.Interfaces
{
    public interface IStatusEffectHolder<in T> where T : IStatusEffect
    {
        void Enable();
        void Disable();
        void Add(T statusEffect);
        void Remove(T statusEffect);
    }
}