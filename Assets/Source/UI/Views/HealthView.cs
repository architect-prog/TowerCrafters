using UnityEngine;

namespace Source.UI.Views
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer healthSprite;

        public void UpdateHealthBar(float remainHealthPercent)
        {
            var healthBarSize = new Vector2(remainHealthPercent, healthSprite.size.y);
            healthSprite.size = healthBarSize;
        }
    }
}