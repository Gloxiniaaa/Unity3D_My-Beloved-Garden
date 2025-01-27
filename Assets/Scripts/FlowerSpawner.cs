using ObjectPool;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    [SerializeField] private ComponentPoolSO<Flower> _flowerPool;
    [Header("Listen on:")]
    [SerializeField] private Vec3EventChannelSO _spawnFlowerChannel;

    // @TODO: implement an object pool for this spawner 

    private void OnEnable()
    {
        _spawnFlowerChannel.OnEventRaised += SpawnFlower;
    }

    private void SpawnFlower(Vector3 spawnPos)
    {
        Flower flower = _flowerPool.Request();
        flower.AssignPool(_flowerPool);
        flower.transform.position = spawnPos;
        flower.Bloom();
    }


    private void OnDisable()
    {
        _spawnFlowerChannel.OnEventRaised -= SpawnFlower;
    }
}