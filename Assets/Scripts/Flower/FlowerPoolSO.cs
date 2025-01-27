using Factory;
using ObjectPool;
using UnityEngine;

[CreateAssetMenu(fileName = "FlowerPool", menuName = "Pool/Flower Pool")]
public class FlowerPoolSO : ComponentPoolSO<Flower>
{
    [SerializeField] private FlowerFactorySO _flowerFactory;
    public override IFactory<Flower> Factory { get { return _flowerFactory; } set { _flowerFactory = value as FlowerFactorySO; } }
}