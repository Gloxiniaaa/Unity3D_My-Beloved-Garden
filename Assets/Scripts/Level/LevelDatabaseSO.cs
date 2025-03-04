using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabaseSO", menuName = "Level/LevelDatabaseSO", order = 0)]
public class LevelDatabaseSO : ScriptableObject
{
    public int SelectedLevelId;
    public int MaxUnlockedLevel;

    [Header("Listen to:")]
    [SerializeField] private BoolEventChannelSO _onCompletionChannel;

    [Tooltip("its order in array is the order in the game")]
    public List<LevelSO> Levels;

    public LevelSO GetSelectedLevel()
    {
        if (SelectedLevelId < Levels.Count)
            return Levels[SelectedLevelId];
        return null;
    }

    void OnEnable()
    {
        _onCompletionChannel.OnEventRaised += UnlockNextLevel;
    }

    private void UnlockNextLevel(bool win)
    {
        if (!win)
            return;
        if (SelectedLevelId <= MaxUnlockedLevel)
        {
            SelectedLevelId++;
            return;
        }
        MaxUnlockedLevel++;
    }

    private void OnDisable()
    {
        _onCompletionChannel.OnEventRaised -= UnlockNextLevel;
    }
}
