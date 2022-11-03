using Source.Common.Messaging;
using Source.Core.Components.Currency.Interfaces;
using Source.Core.Components.Loot;
using Source.Core.Constants;

namespace Source.Core.Components.Currency
{
    public class ObservableEssenceWallet : IWallet<MagicEssence>
    {
        private readonly IWallet<MagicEssence> wallet;

        public int Balance => wallet.Balance;

        public ObservableEssenceWallet(IWallet<MagicEssence> wallet)
        {
            this.wallet = wallet;
        }

        public bool TryMakeTransaction(Transaction<MagicEssence> transaction)
        {
            var isSuccess = wallet.TryMakeTransaction(transaction);
            if (isSuccess)
                MessageBus<int>.Broadcast(Messaging.Events.EssenceTransactionCompleted, Balance);

            return isSuccess;
        }
    }
}