using DG.Tweening;
using UnityEngine;

public class Flower : MonoBehaviour
{
    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    [ContextMenu("Bloom")]
    public void Bloom()
    {
        transform.DOScale(1.0f, 1.5f).SetEase(Ease.OutBack);
    }
}