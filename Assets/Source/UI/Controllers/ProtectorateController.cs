﻿using Source.Common.Messaging;
using Source.Core.Constants;
using Source.UI.Views;
using UnityEngine;

namespace Source.UI.Controllers
{
    public class ProtectorateController : MonoBehaviour
    {
        [SerializeField] private ProtectorateDurabilityView view;

        private void OnEnable()
        {
            MessageBus<int>.AddListener(Messaging.Events.ProtectorateDurabilityUpdated, UpdateDurabilityView);
        }

        private void OnDisable()
        {
            MessageBus<int>.RemoveListener(Messaging.Events.ProtectorateDurabilityUpdated, UpdateDurabilityView);
        }

        private void UpdateDurabilityView(int balance)
        {
            view.UpdateDurability(balance);
        }
    }
}