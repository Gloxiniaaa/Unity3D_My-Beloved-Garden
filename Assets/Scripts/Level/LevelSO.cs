using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "Level/LevelSO")]
public class LevelSO : ScriptableObject 
{
    public int LevelNumber;
    public int FlowerCount;
    public string SceneAddress => "Assets/_Scenes/Levels/" + LevelNumber.ToString() +  ".unity";
    public Vector3 PlayerSpawnPosition;
    public List<GameplayToolSO> GameplayToolSOs;
}