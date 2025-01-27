using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    [SerializeField] private Flower _flowerPrefab;
    [Header("Listen on:")]
    [SerializeField] private Vec3EventChannelSO _spawnFlowerChannel;


    // @TODO: implement an object pool for this spawner 

    private void OnEnable()
    {
        _spawnFlowerChannel.OnEventRaised += SpawnFlower;
    }

    private void SpawnFlower(Vector3 spawnPos)
    {
        Flower flower = Instantiate(_flowerPrefab, spawnPos, Quaternion.identity);
        flower.Bloom();
    }

    private void DespawnFlower()
    {
    }

    private void OnDisable()
    {
        _spawnFlowerChannel.OnEventRaised -= SpawnFlower;
    }
}