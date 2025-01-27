
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Events/Flower Event Channel")]
public class FlowerEventChannelSO : DescriptionBaseSO
{
    public UnityAction<Flower> OnEventRaised;

    public void RaiseEvent(Flower flower)
    {
        OnEventRaised?.Invoke(flower);
    }
}
