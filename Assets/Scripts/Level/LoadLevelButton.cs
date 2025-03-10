using UnityEngine;

public class LoadLevelButton : UIButton
{
    [SerializeField] private LoadLevelType loadLevelType;

    [Header("Broadcast on:")]
    [SerializeField] private BoolEventChannelSO _LoadLevelChannel;

    public void LoadLevel()
    {
        _LoadLevelChannel.RaiseEvent(loadLevelType == LoadLevelType.NEXT_LEVEL);
        PlayClickSfx();
    }
}

public enum LoadLevelType{
    REPLAY_LEVEL = 0,
    NEXT_LEVEL = 1,
}