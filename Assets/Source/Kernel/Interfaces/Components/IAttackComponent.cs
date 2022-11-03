namespace Source.Kernel.Interfaces.Components
{
    public interface IAttackComponent
    {
        float DelayBeforeAttack { get; }
        float AttackFrequency { get; }
        void Attack();
    }
}