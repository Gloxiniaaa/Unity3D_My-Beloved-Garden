using UnityEngine;
using TMPro;
using System;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    private int _levelNumber;
    private LevelSO _levelSetting;
    public static event Action<int> OnLevelSelected;

    public void Setup(LevelSO setting)
    {
        _levelSetting = setting;
        _levelNumber = setting.LevelNumber;
        _levelText.text = _levelNumber.ToString();
    }

    public void SelectLevel()
    {
        OnLevelSelected.Invoke(_levelNumber);
    }
}
