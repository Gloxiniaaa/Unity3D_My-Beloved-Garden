using System.Collections;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    // serves as single entry point for the game
    [Header("UI")]
    [SerializeField] private SetupGameplayToolsUI _gamePlayCanvas;
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _endGameCanvas;
    [SerializeField] private GameObject _eventSystem;

    [Header("Services")]
    [SerializeField] private BGMPlayer _bgmPlayer;
    [SerializeField] private AudioPlayer _sfxPlayer;
    [SerializeField] private FlowerSpawner _flowerSpawner;
    [SerializeField] private FlowerCounter _flowerCounter;
    [SerializeField] private LevelSceneManagerSO _levelSceneManagerSO;

    [Header("Player Preferences")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private CharacterDatabaseSO _characterDatabaseSO;


    [Header("Listen to:")]
    [SerializeField] private BoolEventChannelSO _LoadLevelChannel;


    private PlayerController inGamePlayerController;


    private IEnumerator Start()
    {
        InstantiateCommonServices();
        _levelSceneManagerSO.LoadCurrentLevel();
        yield return new WaitForSeconds(0.5f);
        InitializeLevel();
    }

    void OnEnable()
    {
        _LoadLevelChannel.OnEventRaised += LoadLevel;
    }

    private void LoadLevel(bool isNextLevel)
    {
        if (isNextLevel)
        {
            _levelSceneManagerSO.LoadNextLevel();
        }
        else
        {
            _levelSceneManagerSO.LoadCurrentLevel();
        }
        InitializeLevel();
    }

    private void InstantiateCommonServices()
    {
        Transform uiParents = new GameObject("UI Parents").transform;
        Instantiate(_eventSystem, uiParents);
        Instantiate(_menuCanvas, uiParents);
        Instantiate(_endGameCanvas, uiParents);
        _gamePlayCanvas = Instantiate(_gamePlayCanvas, uiParents);

        Transform services = new GameObject("Services").transform;
        Instantiate(_flowerSpawner, services);
        Instantiate(_sfxPlayer, services);
        Instantiate(_bgmPlayer, services);
        _flowerCounter = Instantiate(_flowerCounter, services);
    }

    private void InitializeLevel()
    {
        LevelSO currentLevelSO = _levelSceneManagerSO.GetCurrentLevelSO();
        if (currentLevelSO == null)
            return;

        _flowerCounter.CountLandSlots(currentLevelSO.FlowerCount);
        
        if (inGamePlayerController) Destroy(inGamePlayerController.gameObject);
        inGamePlayerController = Instantiate(_playerController);
        inGamePlayerController.SetupCharacter(_characterDatabaseSO.GetSelectedCharacterSO().Prefab, currentLevelSO.PlayerSpawnPosition);

        _gamePlayCanvas.DestroyToolUIs();
        foreach (GameplayToolSO gameplayToolSO in currentLevelSO.GameplayToolSOs)
        {
            _gamePlayCanvas.SpawnUI(gameplayToolSO.UIPrefab);
            gameplayToolSO.Setup(inGamePlayerController);
        }
    }

    void OnDisable()
    {
        _LoadLevelChannel.OnEventRaised -= LoadLevel;
    }
}