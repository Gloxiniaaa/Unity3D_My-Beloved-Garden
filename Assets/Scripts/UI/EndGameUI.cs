using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _winGamePannel;
    [SerializeField] private GameObject _loseGamePannel;
    [Header("Listen to:")]
    [SerializeField] private BoolEventChannelSO _onCompletionChannel;
    [SerializeField] private VoidEventChannelSO _onStepOnFlower;

    private CollapsibleButton _winCollapsible;
    private CollapsibleButton _loseCollapsible;

    private void Awake()
    {
        _winCollapsible = _winGamePannel.AddComponent<CollapsibleButton>();
        _loseCollapsible = _loseGamePannel.AddComponent<CollapsibleButton>();
    }

    private void OnEnable()
    {
        _onCompletionChannel.OnEventRaised += ShowPannel;
        _onStepOnFlower.OnEventRaised += ShowLosePanel;
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

    private void ShowLosePanel()
    {
        _loseCollapsible.Toggle();
    }

    private void OnDisable()
    {
        _onCompletionChannel.OnEventRaised -= ShowPannel;
        _onStepOnFlower.OnEventRaised -= ShowLosePanel;
    }
}