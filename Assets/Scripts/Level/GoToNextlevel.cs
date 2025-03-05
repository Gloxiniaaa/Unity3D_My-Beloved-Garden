using UnityEngine;

public class GoToNextLevel : MonoBehaviour
{
    [Header("Broadcast on:")]
    [SerializeField] private VoidEventChannelSO _toNextLevelChannel;

    public void ToNextLevel()
    {
        _toNextLevelChannel.RaiseEvent();
    }

    
}