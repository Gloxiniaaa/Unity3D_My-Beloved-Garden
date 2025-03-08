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
    [SerializeField] private AudioPlayer _bgmPlayer;
    [SerializeField] private AudioPlayer _sfxPlayer;
    [SerializeField] private FlowerSpawner _flowerSpawner;
    [SerializeField] private FlowerCounter _flowerCounter;
    [SerializeField] private LevelSceneManagerSO _levelSceneManagerSO;

    [Header("Player Preferences")]
    [SerializeField] private PlayerController _playerController;

    IEnumerator Start()
    {
        // initialize game UI
        Instantiate(_eventSystem);
        SetupGameplayToolsUI gameplayToolsUI = Instantiate(_gamePlayCanvas);
        Instantiate(_menuCanvas);
        Instantiate(_endGameCanvas);

        // intialize serrcices
        Instantiate(_sfxPlayer);
        Instantiate(_flowerSpawner);

        _levelSceneManagerSO.LoadLevel();
        LevelSO currentLevelSO = _levelSceneManagerSO.GetCurrentLevelSO();

        yield return new WaitForSeconds(0.5f);
        Instantiate(_flowerCounter).CountLandSlots(currentLevelSO.FlowerCount);

        PlayerController playerController = Instantiate(_playerController, currentLevelSO.PlayerSpawnPosition, Quaternion.identity);

        yield return null;
        foreach (GameplayToolSO gameplayToolSO in currentLevelSO.GameplayToolSOs)
        {
            gameplayToolsUI.SpawnUI(gameplayToolSO.UIPrefab);
            gameplayToolSO.Setup(playerController);
        }
    }

}