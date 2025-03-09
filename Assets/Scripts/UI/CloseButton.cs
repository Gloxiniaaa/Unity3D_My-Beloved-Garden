using DG.Tweening;
using UnityEngine;

public class CloseButton : UIButton
{
    [SerializeField] private Transform toClose;

    public void Close()
    {
        PlayClickSfx();
        toClose.transform.DOScale(0, 0.3f)
                        .SetEase(Ease.InBack)
                        .OnComplete(() => toClose.gameObject.SetActive(false));
    }
}