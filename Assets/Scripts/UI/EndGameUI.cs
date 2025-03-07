using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _winGamePannel;
    [SerializeField] private GameObject _loseGamePannel;
    [Header("Listen to:")]
    [SerializeField] private BoolEventChannelSO _onCompletionChannel;

    private CollapsibleUI _winCollapsible;
    private CollapsibleUI _loseCollapsible;

    private void Awake()
    {
        _winCollapsible = _winGamePannel.AddComponent<CollapsibleUI>();
        _loseCollapsible = _loseGamePannel.AddComponent<CollapsibleUI>();
    }

    private void OnEnable()
    {
        _onCompletionChannel.OnEventRaised += ShowPannel;
    }

    private void ShowPannel(bool win)
    {
        if (win)
        {
            _winCollapsible.Toggle();
        }
        else
        {
            _loseCollapsible.Toggle();
        }
    }

    private void OnDisable()
    {
        _onCompletionChannel.OnEventRaised -= ShowPannel;
    }
}