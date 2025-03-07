using UnityEngine;


public class LandSlotsCounter : MonoBehaviour
{
    [Header("Broadcast on:")]
    [SerializeField] private IntEventChannelSO _countLandSlotsChannel;

    private void OnEnable()
    {
        _countLandSlotsChannel.RaiseEvent(transform.childCount);
    }
}