using Source.Core.Components.Currency.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Views.Abstractions
{
    public abstract class BaseWalletView<TCurrency> : MonoBehaviour
        where TCurrency : ICurrency
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI balance;

        private void Start()
        {
            image ??= GetComponentInChildren<Image>();
            balance ??= GetComponentInChildren<TextMeshProUGUI>();
        }

        public void UpdateBalance(int currentBalance)
        {
            balance.SetText(currentBalance.ToString());
        }
    }
}