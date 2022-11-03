using Source.Common.Utils;
using Source.Core.Components.Currency;
using Source.Core.Components.Currency.Interfaces;
using Source.Core.Components.Loot;
using Source.Core.Services;
using Source.Core.Services.Interfaces;
using Source.Kernel.Interfaces;
using Source.Kernel.Services;
using UnityEngine;

namespace Source.Core
{
    public class ApplicationRoot : Singleton<ApplicationRoot>
    {
        public ILogger Logger { get; }
        public IDamageCalculationService DamageCalculationService { get; }
        public IWallet<Coin> Coins { get; }
        public IWallet<MagicEssence> Essence { get; }
        public IObjectCreator ObjectCreator { get; }

        public ApplicationRoot()
        {
            Logger = new Logger(new LogHandler());
            DamageCalculationService = new DamageCalculationService();
            Coins = new ObservableCoinWallet(new Wallet<Coin>());
            Essence = new ObservableEssenceWallet(new Wallet<MagicEssence>());
            ObjectCreator = new ObjectCreator();
        }
    }
}