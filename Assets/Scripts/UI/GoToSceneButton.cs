using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneButton : MonoBehaviour
{
    [SerializeField] private SceneType _sceneType;

    public void GoToScene()
    {
        SceneManager.LoadSceneAsync((int)_sceneType);
    }
}