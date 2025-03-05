using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelSceneManagerSO", menuName = "Level/LevelSceneManagerSO")]
public class LevelSceneManagerSO : ScriptableObject
{
    private int _currentLevelIndex;
    public int MaxUnlockedLevel { get; private set; }
    private AsyncOperationHandle<SceneInstance> _loadedScene;
    [SerializeField] private LevelDatabaseSO _levelDatabaseSO;

    [Header("Listen to:")]
    [SerializeField] private BoolEventChannelSO _onCompletionChannel;
    [SerializeField] private VoidEventChannelSO _toNextLevelChannel;

    [ContextMenu("Reset Value")]
    private void Reset()
    {
        _currentLevelIndex = 0;
        MaxUnlockedLevel = 0;
    }

    private void OnEnable()
    {
        _onCompletionChannel.OnEventRaised += UnlockNextLevel;
        _toNextLevelChannel.OnEventRaised += GotoNextLevel;
    }

    public void SelectLevel(int levelIndex)
    {
        _currentLevelIndex = levelIndex;
    }

    private void GotoNextLevel()
    {
        _currentLevelIndex++;
        SceneManager.LoadSceneAsync((int)SceneType.GAME_PLAY_SCENE);
    }

    public void LoadLevel()
    {
        if (_loadedScene.IsValid())
            Addressables.UnloadSceneAsync(_loadedScene);

        LevelSO LevelToLoad = GetLevelSO(_currentLevelIndex);
        if (LevelToLoad == null)
        {
            SceneManager.LoadScene((int)SceneType.HOME_SCENE);
        }
        else
        {
            _loadedScene = Addressables.LoadSceneAsync(LevelToLoad.SceneAddress, LoadSceneMode.Additive);
        }
    }

    private LevelSO GetLevelSO(int levelIndex)
    {
        if (levelIndex > MaxUnlockedLevel) // prevent player from choosing the locked Level
        {
            return null;
        }

        _currentLevelIndex = levelIndex;
        return _levelDatabaseSO.GetLevel(levelIndex);
    }

    private void UnlockNextLevel(bool win)
    {
        if (!win || _currentLevelIndex < MaxUnlockedLevel)
            return;

        if (MaxUnlockedLevel < _levelDatabaseSO.GetTotalLevels() - 1)
        {
            MaxUnlockedLevel++;
        }
    }

    private void OnDisable()
    {
        _onCompletionChannel.OnEventRaised -= UnlockNextLevel;
        _toNextLevelChannel.OnEventRaised -= GotoNextLevel;
    }
}


