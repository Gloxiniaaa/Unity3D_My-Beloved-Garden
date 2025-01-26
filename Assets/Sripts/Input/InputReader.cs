using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace GameInput
{
    [CreateAssetMenu(fileName = "InputReaderSO", menuName = "Input/Input Reader", order = 0)]
    public class InputReaderSO : ScriptableObject, GameInput.IPlayerActions
    {
        public event UnityAction Left = delegate { };
        public event UnityAction Right = delegate { };
        public event UnityAction Up = delegate { };
        public event UnityAction Down = delegate { };
        public event UnityAction<Vector2> Move = delegate { };
        private GameInput _gameInput;

        private void OnEnable()
        {
            _gameInput = new GameInput();
            _gameInput.Player.SetCallbacks(this);
            _gameInput.Player.Enable();
        }

        public void OnDown(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                Down.Invoke();
        }

        public void OnLeft(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                Left.Invoke();
        }

        public void OnRight(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                Right.Invoke();
        }

        public void OnUp(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                Up.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                Move.Invoke(context.ReadValue<Vector2>());
            }
        }
    }
}