
using UnityEngine;

public class ShovelFlowerCommand : ICommand
{
    private readonly Player _player;
    private Vector3 _direction;
    private readonly Vec3EventChannelSO _spawnFlowerChannel;
    private Vector3 _position;

    public ShovelFlowerCommand(Player player, Vector3 direction, Vec3EventChannelSO spawnFlowerChannel, Vector3 position)
    {
        _player = player;
        _direction = direction;
        _spawnFlowerChannel = spawnFlowerChannel;
        _position = position;
    }
    public void Execute()
    {
        _player.UseShovel(_direction);
    }

    public void Undo()
    {
        _spawnFlowerChannel.RaiseEvent(_position);
    }
}