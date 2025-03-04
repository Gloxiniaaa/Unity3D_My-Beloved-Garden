using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapturer : MonoBehaviour
{
    private Texture2D _screenCapture;
    [SerializeField] private Image _photoDisplayArea;


    [Header("Listen to:")]
    [SerializeField] private BoolEventChannelSO _onCompletionChannel;


    private void Start()
    {
        _screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    void OnEnable()
    {
        _onCompletionChannel.OnEventRaised += TakeScreenShot;
    }

    private void TakeScreenShot(bool arg0)
    {
        StartCoroutine(SreenshotCoroutine());
    }

    private IEnumerator SreenshotCoroutine()
    {
        yield return new WaitForEndOfFrame();
        Rect regionToRead = new Rect(0,0,Screen.width, Screen.height);
        _screenCapture.ReadPixels(regionToRead,0,0,false);
        _screenCapture.Apply();
        _photoDisplayArea.sprite = Sprite.Create(_screenCapture, regionToRead, new Vector2(0.5f, 0.5f), 100f);
    }

    private void OnDisable()
    {
        _onCompletionChannel.OnEventRaised -= TakeScreenShot;
    }
}
