using System.Collections;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    // serves as single entry point for the game
    [Header("UI")]
    [SerializeField] private Canvas _gamePlayCanvas;
    [SerializeField] private Canvas _menuCanvas;
    [SerializeField] private Canvas _endGameCanvas;
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
        Instantiate(_gamePlayCanvas);
        Instantiate(_menuCanvas);
        Instantiate(_endGameCanvas);
        yield return null;

        // intialize serrcices
        Instantiate(_sfxPlayer);
        Instantiate(_flowerSpawner);
        yield return null;

        _levelSceneManagerSO.LoadLevel();

        yield return new WaitForSeconds(1f);
        Instantiate(_flowerCounter).CountLandSlots(_levelSceneManagerSO.GetCurrentLevelSO().FlowerCount);
        
        yield return null;
        // the last thing to do is to initialize the player
        Instantiate(_playerController);
    }

}