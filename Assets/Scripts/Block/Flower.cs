using DG.Tweening;
using UnityEngine;

public class Flower : MonoBehaviour
{
    private Collider _collider;
    private void Awake()
    {
        transform.localScale = Vector3.zero;
        _collider = GetComponent<BoxCollider>();
    }

    [ContextMenu("Bloom")]
    public void Bloom()
    {
        _collider.enabled = true;
        transform.DOScale(1.0f, 1.0f).SetEase(Ease.OutBack);
        transform.DORotate(new Vector3(0, 720, 0), 2.0f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Yoyo);
    }

    [ContextMenu("Reverse Bloom")]
    public void ReverseBloom()
    {
        _collider.enabled = false;
        transform.DOKill();
        transform.DOScale(0, 0.5f).SetEase(Ease.InBack).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.zero;
        transform.DOKill();
    }
}