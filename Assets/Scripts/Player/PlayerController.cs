using GameInput;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader;
    [SerializeField] private VoidEventChannelSO _undoMoveChannel;
    [SerializeField] private Player _player;
    private CommandInvoker _playerCommandInvoker;

    private void Awake()
    {
        _playerCommandInvoker = new CommandInvoker();
    }

    private void OnEnable()
    {
        _inputReader.Move += Move;
        _undoMoveChannel.OnEventRaised += UndoMove;
    }

    private void UndoMove()
    {
        if (_player.CanMove())
        {
            _playerCommandInvoker.UndoCommand();
        }
    }


    private void Move(Vector2 input)
    {
        Vector3 direction = Helper.InputTo3dAxisDirection(input);

        if (_player.CanMove())
        {
            if (Physics.Raycast(_player.transform.position, direction, Constant.GRID_SIZE, 1 << Constant.OBSTACLE_LAYER))
            {
                _player.Move(direction); //it is a fake move so no need to save it in command history
            }
            else
            {
                _playerCommandInvoker.DoAndSaveCommand(new PlayerMoveCommand(_player, direction));
            }
        }
    }

    private void OnDisable()
    {
        _inputReader.Move -= Move;
        _undoMoveChannel.OnEventRaised -= UndoMove;
    }
}