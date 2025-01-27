using Factory;
using UnityEngine;

[CreateAssetMenu(fileName = "FlowerFactory", menuName = "Pool/Flower Factory")]
public class FlowerFactorySO : FactorySO<Flower>
{
    [SerializeField] private Flower _flowerPrefab;
    public override Flower Create()
    {
        return Instantiate(_flowerPrefab);
    }
}