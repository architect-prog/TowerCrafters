using System;
using Source.Common.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace Source.UI.Views
{
    public class AbilityView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Image cooldownMask;
        [SerializeField] private Button button;

        private RepeatingTimer<Func<float>> timer;

        private void Start()
        {
            icon ??= GetComponentInChildren<Image>();
            cooldownMask ??= GetComponentInChildren<Image>();
            timer = new RepeatingTimer<Func<float>>(this)
                .WithAction(UpdateCooldownMask);
        }

        public void Initialize(
            Sprite abilityIcon,
            Action onClick)
        {
            icon.sprite = abilityIcon;
            button.clicked += onClick;
        }

        public void StartCooldown(Func<float> cooldownProvider)
        {
            timer.Start(cooldownProvider);
        }

        private void UpdateCooldownMask(Func<float> cooldownProvider)
        {
            //cooldownMask.image - update percentage
            if (cooldownProvider?.Invoke() <= 0)
                timer.Stop();
        }
    }
}