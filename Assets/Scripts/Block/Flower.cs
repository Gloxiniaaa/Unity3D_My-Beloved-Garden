using DG.Tweening;
using UnityEngine;

public class Flower : MonoBehaviour
{
    private void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        Bloom();
    }

    [ContextMenu("Bloom")]
    public void Bloom()
    {
        transform.DOScale(1.0f, 1.5f).SetEase(Ease.OutBack);
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.zero;
    }
}