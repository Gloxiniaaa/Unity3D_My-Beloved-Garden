using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDatabaseSO", menuName = "Character/CharacterDatabaseSO")]
public class CharacterDatabaseSO : ScriptableObject
{
    private int _selectedCharacterIndex;

    [SerializeField] private List<CharacterSO> characterSOs;

    public void SetSelectedCharacter(int index)
    {
        _selectedCharacterIndex = index;
    }

    public CharacterSO GetCharacterSO(int index)
    {
        if (index < characterSOs.Count)
            return characterSOs[index];
        return null;
    }

    public CharacterSO GetSelectedCharacterSO()
    {
        return characterSOs[_selectedCharacterIndex];
    }

    public int GetSelectedIndex()
    {
        return _selectedCharacterIndex;
    }

    public int GetTotalCharacters()
    {
        return characterSOs.Count;
    }
}