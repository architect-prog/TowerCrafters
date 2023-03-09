using Source.Common.Messaging;
using Source.Core.Components.Loot;
using Source.Core.Constants;
using Source.UI.Views.Abstractions;
using UnityEngine;

namespace Source.UI.Controllers
{
    public class WalletController : MonoBehaviour
    {
        [SerializeField] private BaseWalletView<Coin> coinsWalletView;
        [SerializeField] private BaseWalletView<MagicEssence> essenceWalletView;

        private void Start()
        {
            UpdateCoinsView(0);
            UpdateEssenceView(0);
        }

        private void OnEnable()
        {
            MessageBus<int>.AddListener(Messaging.Events.CoinTransactionCompleted, UpdateCoinsView);
            MessageBus<int>.AddListener(Messaging.Events.EssenceTransactionCompleted, UpdateEssenceView);
        }

        private void OnDisable()
        {
            MessageBus<int>.RemoveListener(Messaging.Events.CoinTransactionCompleted, UpdateCoinsView);
            MessageBus<int>.RemoveListener(Messaging.Events.EssenceTransactionCompleted, UpdateEssenceView);
        }

        private void UpdateCoinsView(int balance)
        {
            coinsWalletView.UpdateBalance(balance);
        }

        private void UpdateEssenceView(int balance)
        {
            essenceWalletView.UpdateBalance(balance);
        }
    }
}