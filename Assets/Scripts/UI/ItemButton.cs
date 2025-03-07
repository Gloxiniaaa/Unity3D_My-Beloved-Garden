using DG.Tweening;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private AudioGroupSO _clickSfx;
    [Header("Broadcast on:")]
    [SerializeField] private VoidEventChannelSO _useItemChannel;
    [SerializeField] private AudioEventChannelSO _uiSfxChannel;

    public void OnClick()
    {
        if (transform.localScale != Vector3.one) // act as a cooldown, preven player from spamming
            return;
        _useItemChannel.RaiseEvent();
        _uiSfxChannel.RaiseEvent(_clickSfx);
        transform.DOScale(Vector3.one * 1.1f, 0.1f).SetLoops(2, LoopType.Yoyo).OnComplete(() => { transform.localScale = Vector3.one; });
    }
}