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
    private bool _previousScene = false;
    [SerializeField] private LevelDatabaseSO _levelDatabaseSO;

    [Header("Listen to:")]
    [SerializeField] private BoolEventChannelSO _onCompletionChannel;
    [SerializeField] private VoidEventChannelSO _toNextLevelChannel;

    [ContextMenu("Reset Value")]
    private void Reset()
    {
        _currentLevelIndex = 0;
        MaxUnlockedLevel = 0;
        _previousScene = false;
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
        Debug.Log($"_previousScene: {_previousScene}");

        if (_previousScene)
        {
            Addressables.UnloadSceneAsync(_loadedScene).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log("Scene Unloaded Successfully");
                }
                else
                {
                    Debug.LogError("Scene Unload Failed: " + handle.OperationException);
                }

                LoadNextLevel();
            };
        }
        else
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        LevelSO LevelToLoad = GetLevelSO(_currentLevelIndex);

        if (LevelToLoad == null)
        {
            SceneManager.LoadSceneAsync((int)SceneType.HOME_SCENE);
        }
        else
        {
            _previousScene = true;
            _loadedScene = Addressables.LoadSceneAsync(LevelToLoad.SceneAddress, LoadSceneMode.Additive);
            _loadedScene.Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log("Scene Loaded Successfully");
                }
                else
                {
                    Debug.LogError("Scene Load Failed: " + handle.OperationException);
                }
            };
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

    public LevelSO GetCurrentLevelSO()
    {
        return _levelDatabaseSO.GetLevel(_currentLevelIndex);
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


