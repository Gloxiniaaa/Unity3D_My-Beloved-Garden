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
        transform.DOScale(1.0f, 1.0f).SetEase(Ease.OutBack);
        transform.DORotate(new Vector3(0, 720, 0), 2.0f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.zero;
        transform.DOKill();
    }
}