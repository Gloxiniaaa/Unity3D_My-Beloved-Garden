using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "LevelSO", order = 0)]
public class LevelSO : ScriptableObject
{
    public int LevelNumber;
    public int FlowerCount;
    public string SceneAddress => "Assets/Scenes/Levels/" + LevelNumber.ToString() +  ".unity";
    public Vector3 PlayerSpawnPosition;
}