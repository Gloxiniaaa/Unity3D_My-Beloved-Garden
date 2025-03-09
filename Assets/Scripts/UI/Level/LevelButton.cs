using UnityEngine;
using TMPro;
using System;

public class LevelButton : UIButton
{
    [SerializeField] private TextMeshProUGUI _levelText;
    private int _levelNumber;
    public static event Action<int> OnLevelSelected;

    public void Setup(int levelIndex)
    {
        _levelNumber = levelIndex;
        _levelText.text = _levelNumber.ToString();
    }

    public void SelectLevel()
    {
        OnLevelSelected.Invoke(_levelNumber);
        PlayClickSfx();
    }
}
