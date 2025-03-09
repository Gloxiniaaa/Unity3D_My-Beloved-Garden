using DG.Tweening;
using UnityEngine;

public class ItemButton : UIButton
{
    [Header("Broadcast on:")]
    [SerializeField] private VoidEventChannelSO _useItemChannel;

    public void OnClick()
    {
        if (transform.localScale != Vector3.one) // act as a cooldown, preven player from spamming
            return;
        PlayClickSfx();
        _useItemChannel.RaiseEvent();
        transform.DOScale(Vector3.one * 1.1f, 0.1f).SetLoops(2, LoopType.Yoyo).OnComplete(() => { transform.localScale = Vector3.one; });
    }
}