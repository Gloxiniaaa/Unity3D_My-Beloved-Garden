using UnityEngine;

public class LevelButtonGenerator : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private LevelButton _levelButtonPrefab;
    [SerializeField] private LevelSceneManagerSO _levelSceneManagerSO;

    void OnEnable()
    {
        LevelButton.OnLevelSelected += SetSelectedLevel;
    }

    private void SetSelectedLevel(int levelIndex)
    {
        _levelSceneManagerSO.SelectLevel(levelIndex);
    }

    void Start()
    {
        PopulateLevels();
    }

    void PopulateLevels()
    {

        for (int i = 0; i <= _levelSceneManagerSO.MaxUnlockedLevel; i++)
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
