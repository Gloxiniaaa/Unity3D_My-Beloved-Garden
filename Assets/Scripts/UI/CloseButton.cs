using DG.Tweening;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    [SerializeField] private Transform toClose;

    public void Close()
    {
        toClose.transform.DOScale(0, 0.3f)
                        .SetEase(Ease.InBack)
                        .OnComplete(() => toClose.gameObject.SetActive(false));
    }
}