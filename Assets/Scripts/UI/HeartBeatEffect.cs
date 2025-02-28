using UnityEngine;
using DG.Tweening;

public class HeartBeatEffect : MonoBehaviour
{
    private readonly float _duration = 0.5f;
    private readonly float _interval = 2f;


    void Start()
    {
        Beat();
    }

    void Beat()
    {
        Sequence blinkSequence = DOTween.Sequence();
        blinkSequence.Append(transform.DOScaleY(1.5f, _duration / 2).SetEase(Ease.InOutBack))
                     .Join(transform.DOScaleX(0.95f, _duration / 2).SetEase(Ease.InOutBack))
                     .Append(transform.DOScale(1, _duration / 2))
                     .AppendInterval(_interval)
                     .SetLoops(-1, LoopType.Restart);
    }
}
