using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabaseSO", menuName = "Level/LevelDatabaseSO", order = 0)]
public class LevelDatabaseSO : ScriptableObject
{
    [Tooltip("its order in array is the order in the game")]
    [SerializeField] private List<LevelSO> Levels;

    public LevelSO GetLevel(int levelIndex)
    {
        if (levelIndex < Levels.Count)
            return Levels[levelIndex];
        return null;
    }

    public int GetTotalLevels()
    {
        return Levels.Count;
    }
}
