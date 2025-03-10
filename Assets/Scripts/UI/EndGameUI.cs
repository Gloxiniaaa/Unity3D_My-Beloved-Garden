using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _winGameBtn;
    [SerializeField] private GameObject _loseGameBtn;
    [Header("Listen to:")]
    [SerializeField] private BoolEventChannelSO _onCompletionChannel;
    [SerializeField] private BoolEventChannelSO _LoadLevelChannel;

    private CollapsibleUI _wrapperPannel;

    private void Awake()
    {
        _wrapperPannel = transform.GetChild(0).AddComponent<CollapsibleUI>();
    }

    private void OnEnable()
    {
        _onCompletionChannel.OnEventRaised += ShowPannel;
        _LoadLevelChannel.OnEventRaised += HidePannel;
    }

    private void HidePannel(bool arg0)
    {
        _wrapperPannel.Hide();
    }

    private void ShowPannel(bool win)
    {
        _winGameBtn.SetActive(win);
        _loseGameBtn.SetActive(!win);
        StartCoroutine(WaitAndShow());
    }

    private IEnumerator WaitAndShow()
    {
        yield return new WaitForSeconds(0.5f);
        _wrapperPannel.Show();
    }

    private void OnDisable()
    {
        _onCompletionChannel.OnEventRaised -= ShowPannel;
        _LoadLevelChannel.OnEventRaised -= HidePannel;
    }
}