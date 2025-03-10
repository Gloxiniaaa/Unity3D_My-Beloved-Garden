using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneButton : UIButton
{
    [SerializeField] private SceneType _sceneType;

    public void GoToScene()
    {
        if (_sceneType == SceneType.QUIT_GAME)
        {
            Application.Quit();
            return;
        }
        PlayClickSfx();
        SceneManager.LoadSceneAsync((int)_sceneType);
    }
}

public enum SceneType
{
    QUIT_GAME = -1,
    START_SCENE = 0,
    HOME_SCENE = 1,
    GAME_PLAY_SCENE = 2
}