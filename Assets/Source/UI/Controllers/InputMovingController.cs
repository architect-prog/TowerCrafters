using Source.Kernel.Interfaces.Components;
using Source.UI.Providers;
using Source.UI.Providers.Interfaces;
using UnityEngine;

namespace Source.UI.Controllers
{
    [RequireComponent(typeof(IMovingComponent))]
    [RequireComponent(typeof(IRotatingComponent))]
    public class InputMovingController : MonoBehaviour
    {
        private Vector2 movingDirection;

        private IInputProvider inputProvider;
        private IMovingComponent movingComponent;
        private IRotatingComponent rotatingComponent;

        private void Start()
        {
            movingComponent = GetComponent<IMovingComponent>();
            rotatingComponent = GetComponent<IRotatingComponent>();
        }

        private void Update()
        {
            movingDirection = inputProvider.GetMovementDirection();
        }

        private void FixedUpdate()
        {
            movingComponent.Move(movingDirection);
            rotatingComponent.Rotate(movingDirection);
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