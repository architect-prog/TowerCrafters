using Source.Kernel.Constants;
using Source.Kernel.Contracts;
using Source.Kernel.Data;
using Source.Kernel.Interfaces.Providers;

namespace Source.Core.Components.StatusEffects
{
    public class EnemyDataProxy :
        ISpeedProvider,
        IHealthProvider,
        IResistProvider,
        IDamageProvider,
        IProtectorateDamageProvider,
        IAttackFrequencyProvider
    {
        public ModifiableStat<DamageAmount> Damage { get; }
        public ModifiableStat<ResistAmount> Resist { get; }
        public ModifiableStat<float> MaxHealth { get; }
        public ModifiableStat<float> Speed { get; }

        public DamageAmount TotalDamage => Damage.ModifiedValue;
        public ResistAmount TotalResist => Resist.ModifiedValue;
        public float TotalSpeed => Speed.ModifiedValue;
        public float TotalMaxHealth => MaxHealth.ModifiedValue;
        public int ProtectorateDamage { get; }
        public float AttackFrequency { get; }

        public EnemyDataProxy(EnemyData data)
        {
            Damage = new ModifiableStat<DamageAmount>(data.BaseDamage, Aggregators.DamageAmountSum);
            Resist = new ModifiableStat<ResistAmount>(data.Resist, Aggregators.ResistAmountSum);
            MaxHealth = new ModifiableStat<float>(data.MaxHealth, Aggregators.FloatSum);
            Speed = new ModifiableStat<float>(data.Speed, Aggregators.FloatSum);

            ProtectorateDamage = data.ProtectorateDamage;
            AttackFrequency = data.AttackFrequency;
        }

    }
}