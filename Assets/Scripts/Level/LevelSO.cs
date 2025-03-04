using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Level/LevelSO")]
public class LevelSO : ScriptableObject 
{
    public int LevelNumber;
    public int FlowerCount;
    public string SceneAddress => "Assets/Scenes/Levels/" + LevelNumber.ToString() +  ".unity";
    public Vector3 PlayerSpawnPosition;
}