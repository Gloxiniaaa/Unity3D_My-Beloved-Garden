using System;
using GameInput;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader;
    [SerializeField] private VoidEventChannelSO _undoMoveChannel;
    [SerializeField] private VoidEventChannelSO _useShovelChannel;
    [SerializeField] private Vec3EventChannelSO _spawnFlowerChannel;

    [SerializeField] private Player _player;
    private CommandInvoker _playerCommandInvoker;
    private bool _isShoveling = false;

    private void Awake()
    {
        _playerCommandInvoker = new CommandInvoker();
    }

    private void OnEnable()
    {
        _inputReader.Move += Move;
        _undoMoveChannel.OnEventRaised += UndoMove;
        _useShovelChannel.OnEventRaised += ToggleShoveling;

    }

    private void ToggleShoveling()
    {
        _isShoveling = !_isShoveling;
        if (_isShoveling)
        {
            OnStartShoveling();
        }
        else
        {
            OnEndShoveling();
        }
    }

    private void OnStartShoveling()
    {
        _inputReader.Move -= Move;
        _inputReader.Move += UseShovel;
    }

    private void OnEndShoveling()
    {
        _inputReader.Move -= UseShovel;
        _inputReader.Move += Move;
    }

    private void UseShovel(Vector2 input)
    {
        Vector3 direction = Helper.InputTo3dAxisDirection(input);

        if (_player.CanMove())
        {
            _playerCommandInvoker.DoAndSaveCommand(new ShovelFlowerCommand(_player, direction, _spawnFlowerChannel, _player.transform.position + direction));
        }
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
            if (Physics.Raycast(_player.transform.position, direction, Constant.GRID_SIZE, Constant.OBSTACLE_LAYER_MASK))
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
        _useShovelChannel.OnEventRaised -= ToggleShoveling;
    }
}