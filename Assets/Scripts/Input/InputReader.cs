using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace GameInput
{
    [CreateAssetMenu(fileName = "InputReaderSO", menuName = "Input/Input Reader", order = 0)]
    public class InputReaderSO : ScriptableObject, GameInput.IPlayerActions
    {
        private GameInput _gameInput;
        private InputAction _touchPositionAction;
        private readonly float _swipeDistanceThreshold = 60f;
        private readonly float _timeThreshold = 0.5f;
        private Vector2 _startTouchPosition;
        private float _touchStartTime;

        public event UnityAction<Vector2> Move = delegate { };
        [SerializeField] private VoidEventChannelSO _undoChannel;
        [SerializeField] private VoidEventChannelSO _toggleShovelingChannel;


        private void OnEnable()
        {
            _gameInput = new GameInput();
            _gameInput.Player.SetCallbacks(this);
            _gameInput.Player.Enable();
            _touchPositionAction = _gameInput.Player.PrimaryPosition;
        }

        public void OnMove(InputAction.CallbackContext context) //WASD
        {
            if (context.phase == InputActionPhase.Performed)
            {
                Move.Invoke(context.ReadValue<Vector2>());
            }
        }

        public void OnPrimaryContact(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                _startTouchPosition = _touchPositionAction.ReadValue<Vector2>();
                _touchStartTime = Time.time;
            }
            if (context.phase == InputActionPhase.Canceled)
            {
                if (Time.time - _touchStartTime > _timeThreshold)
                {
                    return; // Swipe too slow, ignore
                }

                Vector2 endTouchPosition = _touchPositionAction.ReadValue<Vector2>();
                Vector2 swipeDelta = endTouchPosition - _startTouchPosition;

                if (swipeDelta.magnitude > _swipeDistanceThreshold)
                {
                    Move.Invoke(GetSwipeDirection(swipeDelta));
                }
            }
        }

        private Vector2 GetSwipeDirection(Vector2 swipeDelta)
        {
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                return new Vector2(swipeDelta.x, 0).normalized;
            }
            else
            {
                return new Vector2(0, swipeDelta.y).normalized;
            }
        }
        public void OnPrimaryPosition(InputAction.CallbackContext context)
        {
            // throw new System.NotImplementedException();
        }

        public void OnUnDo(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                _undoChannel.RaiseEvent();
            }
        }

        public void OnToggleShoveling(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                _toggleShovelingChannel.RaiseEvent();
            }
        }
    }
}