namespace Source.Core.Components.StatusEffects.Interfaces
{
    public interface ITemporaryStatusEffect : IStatusEffect
    {
        float Duration { get; }
        float RemainingDuration { get; set; }
    }
}