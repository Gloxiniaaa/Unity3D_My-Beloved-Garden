using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    [SerializeField] private Flower _flowerPrefab;
    [Header("Listen on:")]
    [SerializeField] private Vec3EventChannelSO _spawnFlowerChannel;

    private void OnEnable()
    {
        _spawnFlowerChannel.OnEventRaised += SpawnFlower;
    }

    private void SpawnFlower(Vector3 spawnPos)
    {
        Instantiate(_flowerPrefab, spawnPos, Quaternion.identity);
    }

    private void OnDisable()
    {
        _spawnFlowerChannel.OnEventRaised -= SpawnFlower;
    }
}