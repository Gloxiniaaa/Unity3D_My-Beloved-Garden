using UnityEngine;

public class LevelButtonGenerator : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private LevelButton _levelButtonPrefab;
    [SerializeField] private LevelDatabaseSO _levelDatabase;

    void OnEnable()
    {
        LevelButton.OnLevelSelected += SetSelectedLevel;
    }

    private void SetSelectedLevel(int leveNumber)
    {
        _levelDatabase.SelectedLevelId = leveNumber;
    }

    void Start()
    {
        PopulateLevels();
    }

    void PopulateLevels()
    {
        foreach (LevelSO setting in _levelDatabase.Levels)
        {
            LevelButton newButton = Instantiate(_levelButtonPrefab, _container);
            newButton.Setup(setting);
        }
    }

    void OnDisable()
    {
        LevelButton.OnLevelSelected += SetSelectedLevel;
    }
}
