using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Events/Vector3 Event Channel")]
public class Vec3EventChannelSO : DescriptionBaseSO
{
    public UnityAction<Vector3> OnEventRaised;

    public void RaiseEvent(Vector3 value)
    {
        OnEventRaised?.Invoke(value);
    }
}
