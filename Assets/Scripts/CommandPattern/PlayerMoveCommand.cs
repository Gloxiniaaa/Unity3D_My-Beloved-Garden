
using UnityEngine;

public class PlayerMoveCommand : ICommand
{
    private readonly Player _player;
    private Vector3 _direction;

    public PlayerMoveCommand(Player player, Vector3 direction)
    {
        _player = player;
        _direction = direction;
    }
    public void Execute()
    {
        _player.Move(_direction);
    }

    public void Undo()
    {
        _player.ReverseMove(_direction);
    }
}