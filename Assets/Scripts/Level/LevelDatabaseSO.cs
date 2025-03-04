using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabaseSO", menuName = "Level/LevelDatabaseSO", order = 0)]
public class LevelDatabaseSO : ScriptableObject
{
    public int SelectedLevelId;
    [Tooltip("its order in array is the order in the game")]
    public LevelSO[] Levels;

    public LevelSO GetSelectedLevel()
    {
        return Levels[SelectedLevelId];
    }


}
