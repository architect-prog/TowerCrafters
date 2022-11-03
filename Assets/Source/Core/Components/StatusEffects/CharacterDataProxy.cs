using Source.Kernel.Constants;
using Source.Kernel.Contracts;
using Source.Kernel.Data;
using Source.Kernel.Interfaces.Providers;

namespace Source.Core.Components.StatusEffects
{
    public class CharacterDataProxy :
        ISpeedProvider,
        IHealthProvider,
        IResistProvider,
        IDamageProvider,
        ICollectingRangeProvider,
        IAttackFrequencyProvider
    {
        private ModifiableStat<DamageAmount> Damage { get; }
        private ModifiableStat<ResistAmount> Resist { get; }
        private ModifiableStat<float> MaxHealth { get; }
        private ModifiableStat<float> Speed { get; }

        public DamageAmount TotalDamage => Damage.ModifiedValue;
        public ResistAmount TotalResist => Resist.ModifiedValue;
        public float TotalSpeed => Speed.ModifiedValue;
        public float TotalMaxHealth => MaxHealth.ModifiedValue;
        public float CollectingRange { get; }
        public float AttackFrequency { get; }

        public CharacterDataProxy(CharacterData data)
        {
            Damage = new ModifiableStat<DamageAmount>(data.BaseDamage, Aggregators.DamageAmountSum);
            Resist = new ModifiableStat<ResistAmount>(data.Resist, Aggregators.ResistAmountSum);
            MaxHealth = new ModifiableStat<float>(data.MaxHealth, Aggregators.FloatSum);
            Speed = new ModifiableStat<float>(data.Speed, Aggregators.FloatSum);

            CollectingRange = data.CollectingRange;
            AttackFrequency = data.AttackFrequency;
        }

    }
}