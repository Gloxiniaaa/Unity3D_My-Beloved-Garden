using DG.Tweening;
using UnityEngine;

public class EndGameState : IState
{
    private readonly Player _host;
    private readonly float _moveDuration = 0.3f;
    private readonly Animator _animator;

    protected int _triggerAnimHash;

    public EndGameState(Player host, Animator animator)
    {
        _host = host;
        _animator = animator;
    }

    public void SetType(bool win)
    {
        _triggerAnimHash = win?Animator.StringToHash("yes"):Animator.StringToHash("no");
    }

    public void OnEnter()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() => { _animator.SetTrigger(_triggerAnimHash); });
        sequence.AppendInterval(0.5f);

        Vector3 lookat = Camera.main.transform.position - _host.transform.position;
        lookat.y = 0;
        sequence.Append(RotateToDirection(lookat)); // faceing the camera
    }

    public void OnExit()
    {
    }


    public Tween RotateToDirection(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Vector3 deltaRotation = targetRotation.eulerAngles - _host.transform.rotation.eulerAngles;
        _host.transform.DOKill();
        return _host.transform.DOBlendableRotateBy(deltaRotation, _moveDuration)
           .SetEase(Ease.OutQuad)
           .SetUpdate(UpdateType.Normal, true);

    }
    public void Tick()
    {
    }
}