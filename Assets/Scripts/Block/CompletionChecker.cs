using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CompletionChecker : MonoBehaviour
{
    [SerializeField] private FlowerCounterSO _flowerCounterSO;

    [Header("Broadcast on:")]
    [SerializeField] private BoolEventChannelSO _onCompletionChannel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.PLAYER_TAG))
        {
            if (_flowerCounterSO.AvailableLandSlots == 0)
            {
                _onCompletionChannel.RaiseEvent(true);
                Debug.Log("You have completed the level");
            }
            else
            {
                _onCompletionChannel.RaiseEvent(false);
                Debug.Log("Missing " + (_flowerCounterSO.AvailableLandSlots) + " flowers");
            }
        }
    }
}