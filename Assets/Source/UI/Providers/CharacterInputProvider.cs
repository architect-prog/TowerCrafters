using Source.Common.Utils;
using Source.Input.Actions;
using Source.UI.Providers.Interfaces;
using UnityEngine;

namespace Source.UI.Providers
{
    public class CharacterInputProvider : Singleton<CharacterInputProvider>, IInputProvider
    {
        private readonly InputActions inputActions;

        public CharacterInputProvider()
        {
            inputActions = new InputActions();
            inputActions.Enable();
        }

        public Vector2 GetMovementDirection()
        {
            var input = inputActions.Player.Move.ReadValue<Vector2>();
            return input;
        }

        public Vector2 GetMouseClickPosition()
        {
            var input = inputActions.Player.Click.ReadValue<Vector2>();
            return input;
        }

        public void Dispose()
        {
            inputActions.Disable();
        }
    }
}