using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace GameInput
{
    [CreateAssetMenu(fileName = "InputReaderSO", menuName = "Input/Input Reader", order = 0)]
    public class InputReaderSO : ScriptableObject, GameInput.IPlayerActions
    {
        private GameInput _gameInput;
        private InputAction touchPositionAction;
        private readonly float swipeDistanceThreshold = 60f;
        private readonly float timeThreshold = 0.5f;
        private Vector2 startTouchPosition;
        private float touchStartTime;
        [SerializeField] private VoidEventChannelSO _undoSpawnFlowerChannel;

        public event UnityAction<Vector2> Move = delegate { };

        private void OnEnable()
        {
            _gameInput = new GameInput();
            _gameInput.Player.SetCallbacks(this);
            _gameInput.Player.Enable();
            touchPositionAction = _gameInput.Player.PrimaryPosition;
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
                startTouchPosition = touchPositionAction.ReadValue<Vector2>();
                touchStartTime = Time.time;
            }
            if (context.phase == InputActionPhase.Canceled)
            {
                if (Time.time - touchStartTime > timeThreshold)
                {
                    return; // Swipe too slow, ignore
                }

                Vector2 endTouchPosition = touchPositionAction.ReadValue<Vector2>();
                Vector2 swipeDelta = endTouchPosition - startTouchPosition;

                if (swipeDelta.magnitude > swipeDistanceThreshold)
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
                _undoSpawnFlowerChannel.RaiseEvent();
            }
        }
    }
}