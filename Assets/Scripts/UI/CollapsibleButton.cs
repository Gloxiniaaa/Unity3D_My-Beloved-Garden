using DG.Tweening;
using UnityEngine;

public class CollapsibleButton : MonoBehaviour
{
    private readonly float _duration = 0.2f;
    private bool _isCollapsed;

    void Start()
    {
        Hide();
        _isCollapsed = true;
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

        _isCollapsed = !_isCollapsed;
    }

    private void Show()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, _duration).SetEase(Ease.OutBack);
    }

    private void Hide()
    {
        transform.localScale = Vector3.one;
        transform.DOScale(0, _duration).SetEase(Ease.InBack);
    }
}