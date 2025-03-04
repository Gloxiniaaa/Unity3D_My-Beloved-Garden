using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
public class GameInitiator : MonoBehaviour
{
    // serves as single entry point for the game
    [Header("UI")]
    [SerializeField] private Canvas _gamePlayCanvas;
    [SerializeField] private Canvas _menuCanvas;
    [SerializeField] private Canvas _endGameCanvas;

    [Header("Services")]
    [SerializeField] private GameObject _eventSystem;
    [SerializeField] private AudioPlayer _bgmPlayer;
    [SerializeField] private AudioPlayer _sfxPlayer;
    [SerializeField] private FlowerSpawner _flowerSpawner;

    [Header("Player Preferences")]
    [SerializeField] private LevelDatabaseSO _levelDatabaseSO;
    [SerializeField] private PlayerController _playerController;
    private LevelSO _selectedLevelSO;
    
    IEnumerator Start()
    {
        // setup
        _selectedLevelSO = _levelDatabaseSO.GetSelectedLevel();

        // initialize game UI
        Instantiate(_eventSystem);
        Instantiate(_gamePlayCanvas);
        Instantiate(_menuCanvas);
        Instantiate(_endGameCanvas);
        yield return null;

        // intialize serrcices
        Instantiate(_sfxPlayer);
        Instantiate(_flowerSpawner);
        yield return null;

        Addressables.LoadSceneAsync(_selectedLevelSO.SceneAddress, LoadSceneMode.Additive);
        yield return null;

        // the last thing to do is to initialize the player
        Instantiate(_playerController);
    }

}