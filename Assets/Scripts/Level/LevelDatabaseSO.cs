using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabaseSO", menuName = "Level/LevelDatabaseSO", order = 0)]
public class LevelDatabaseSO : ScriptableObject
{
    public int SelectedLevel;
    [Tooltip("its order in array is the order in the game")]
    public LevelSO[] levels;
}
