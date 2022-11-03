using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Views
{
    public class ProtectorateDurabilityView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI durability;

        private void Start()
        {
            image ??= GetComponentInChildren<Image>();
            durability ??= GetComponentInChildren<TextMeshProUGUI>();
        }

        public void UpdateDurability(int currentDurability)
        {
            durability.SetText(currentDurability.ToString());
        }
    }
}