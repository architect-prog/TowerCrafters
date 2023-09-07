using UnityEngine;
using UnityEngine.InputSystem;

namespace Source.Core.Sandbox
{
    public class InputSpawner : MonoBehaviour
    {
        [SerializeField] private Key spawnKey;
        [SerializeField] private GameObject prefab;
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Keyboard.current[spawnKey].wasPressedThisFrame)
            {
                var inputPosition = Mouse.current.position.ReadValue();

                var position = mainCamera.ScreenToWorldPoint(inputPosition);
                ApplicationRoot.Instance.ObjectCreator.Create(prefab, position);
            }
        }
    }
}