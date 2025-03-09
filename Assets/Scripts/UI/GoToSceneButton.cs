using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneButton : UIButton
{
    [SerializeField] private SceneType _sceneType;

    public void GoToScene()
    {
        PlayClickSfx();
        SceneManager.LoadSceneAsync((int)_sceneType);
    }
}