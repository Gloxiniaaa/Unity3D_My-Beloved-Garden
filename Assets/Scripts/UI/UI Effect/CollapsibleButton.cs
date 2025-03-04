using DG.Tweening;
using UnityEngine;

public class CollapsibleButton : MonoBehaviour
{
    private readonly float _duration = 0.2f;
    private bool _isCollapsed;

    void Start()
    {
        Hide();
    }

    public void Toggle()
    {
        if (_isCollapsed)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, _duration).SetEase(Ease.OutBack);
        _isCollapsed = false;
    }

    private void Hide()
    {
        transform.localScale = Vector3.one;
        transform.DOScale(0, _duration).SetEase(Ease.InBack);
        _isCollapsed = true;
    }
}