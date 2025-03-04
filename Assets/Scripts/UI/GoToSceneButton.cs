using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneButton : MonoBehaviour
{
    [SerializeField] private int _sceneIdx;
    
    public void GoToScene()
    {
        SceneManager.LoadSceneAsync(_sceneIdx);
    }
}