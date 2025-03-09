using UnityEngine;
using TMPro;
using System;

public class CharacterButton : UIButton
{
    [SerializeField] private TextMeshProUGUI _text;
    private int _characterIndex;
    public static event Action<int> OnCharacterSelected;

    public void Setup(int index)
    {
        _characterIndex = index;
        _text.text = ((char)(_characterIndex + 'A')).ToString();
    }

    public void SelectCharacter()
    {
        OnCharacterSelected.Invoke(_characterIndex);
        PlayClickSfx();
    }
}
