using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    [SerializeField] private Flower _flowerPrefab;
    [Header("Listen on:")]
    [SerializeField] private Vec3EventChannelSO _spawnFlowerChannel;
    [SerializeField] private VoidEventChannelSO _undoSpawnFlowerChannel;
    private CommandInvoker _flowerCommandInvoker;


    // @TODO: implement an object pool for this spawner 

    private void Awake()
    {
        _flowerCommandInvoker = new CommandInvoker();
    }

    private void OnEnable()
    {
        _spawnFlowerChannel.OnEventRaised += SpawnFlower;
        _undoSpawnFlowerChannel.OnEventRaised += DespawnFlower;
    }

    private void SpawnFlower(Vector3 spawnPos)
    {
        Flower flower = Instantiate(_flowerPrefab, spawnPos, Quaternion.identity);
        _flowerCommandInvoker.DoCommand(new FlowerBloomCommand(flower));
    }

    private void DespawnFlower()
    {
        _flowerCommandInvoker.UndoCommand();
    }

    private void OnDisable()
    {
        _spawnFlowerChannel.OnEventRaised -= SpawnFlower;
        _undoSpawnFlowerChannel.OnEventRaised -= DespawnFlower;
    }
}