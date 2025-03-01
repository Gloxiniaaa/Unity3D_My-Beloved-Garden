using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "CharacterSO", order = 0)]
public class CharacterSO : ScriptableObject
{
    public string CharacterName;
    public Sprite CharacterSprite;
    public GameObject IdlePrefab;
    public GameObject InGamePrefab;
    
}