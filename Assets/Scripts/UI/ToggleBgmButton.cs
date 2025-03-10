using UnityEngine;

public class ToggleBgmButton : UIButton
{
    [Header("Broadcast on:")]
    [SerializeField] private VoidEventChannelSO _toggleBGMChannel;

    public void Toggle()
    {
        _toggleBGMChannel.RaiseEvent();
        PlayClickSfx();
    }
}