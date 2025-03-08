using UnityEngine;

public abstract class GameplayToolSO : ScriptableObject
{
    public GameObject UIPrefab;
    public abstract void Setup(PlayerController playerController);
}