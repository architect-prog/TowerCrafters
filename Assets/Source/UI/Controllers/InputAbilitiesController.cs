using Source.Core.Components.Abilities;
using Source.Core.Components.Abilities.Contracts;
using Source.Core.Components.Abilities.Interfaces;
using Source.Kernel.Interfaces.Components;
using Source.UI.Providers;
using Source.UI.Providers.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.UI.Controllers
{
    [RequireComponent(typeof(IMovingComponent))]
    [RequireComponent(typeof(IRotatingComponent))]
    public class InputAbilitiesController : MonoBehaviour
    {
        private Vector2 movingDirection;

        private IInputProvider inputProvider;
        private IAbilityExecutor abilityExecutor;

        [SerializeField] private LightningRay lightningRay;

        private void Start()
        {
            abilityExecutor = GetComponent<IAbilityExecutor>();
        }

        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                var inputPosition = Mouse.current.position.ReadValue();
                var worldPosition = Camera.main.ScreenToWorldPoint(inputPosition);

                abilityExecutor.Execute(lightningRay, new AbilityExecutingData()
                {
                    CastPosition = worldPosition
                });
            }
        }

        private void OnEnable()
        {
            inputProvider = CharacterInputProvider.Instance;
        }

        private void OnDisable()
        {
            inputProvider.Dispose();
        }
    }
}