using TMPro;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private LevelButton _levelButtonPrefab;
    [SerializeField] private LevelSceneManagerSO _levelSceneManagerSO;
    [SerializeField] private TextMeshProUGUI _selectedLevelText;

    void OnEnable()
    {
        LevelButton.OnLevelSelected += SetSelectedLevel;
    }

    private void SetSelectedLevel(int levelIndex)
    {
        _selectedLevelText.text = levelIndex.ToString();
        _levelSceneManagerSO.SelectLevel(levelIndex);
    }

    void Start()
    {
        PopulateLevels(_levelSceneManagerSO.MaxUnlockedLevel);
        SetSelectedLevel(_levelSceneManagerSO.MaxUnlockedLevel);
    }

    void PopulateLevels(int numberOfLevels)
    {
        for (int i = 0; i <= numberOfLevels; i++)
        {
            LevelButton newButton = Instantiate(_levelButtonPrefab, _container);
            newButton.Setup(i);
        }
    }

    void OnDisable()
    {
        LevelButton.OnLevelSelected += SetSelectedLevel;
    }
}
