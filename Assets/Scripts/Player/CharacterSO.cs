using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Character/CharacterSO")]
public class CharacterSO : ScriptableObject
{
    public string CharacterName;
    public Sprite CharacterSprite;
    public Player Prefab;
    private Player _spawnedInstance;

    public Player SpawnCharacter()
    {
        _spawnedInstance = Instantiate(Prefab);
        return _spawnedInstance;
    }

    public Player GetCharacter()
    {
        return _spawnedInstance;
    }
    
}