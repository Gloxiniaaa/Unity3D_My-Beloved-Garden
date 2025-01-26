using DG.Tweening;
using UnityEngine;

public class MoveState : IState
{
    private Player _host;
    private Animator _animator;
    private readonly float _moveDuration = 0.3f;
    private readonly float _jumpPower = 0.5f;
    private int _BoolMoveAnimHash = Animator.StringToHash("isMoving");
    private Tween _currentRotationTween;

    public MoveState(Player host, Animator animator)
    {
        _host = host;
        _animator = animator;
    }

    public void Tick()
    {
    }

    public void OnEnter()
    {
        _animator.SetBool(_BoolMoveAnimHash, true);

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(_moveDuration);
        sequence.Join(RotateToDirection(_host.TargetDirection));
        sequence.Join(JumpInDirection(_host.TargetDirection));
        sequence.AppendCallback(() => { _animator.SetBool(_BoolMoveAnimHash, false); });
    }

    public Tween RotateToDirection(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        Vector3 deltaRotation = targetRotation.eulerAngles - _host.transform.rotation.eulerAngles;

        _currentRotationTween?.Kill();

        _currentRotationTween = _host.transform.DOBlendableRotateBy(deltaRotation, _moveDuration)
            .SetEase(Ease.OutQuad)
            .SetUpdate(UpdateType.Normal, true);

        return _currentRotationTween;
    }
    public Tween JumpInDirection(Vector3 direction)
    {
        direction.Normalize();

        Vector3 jumpEndpoint = _host.transform.position + direction;

        jumpEndpoint.y = _host.transform.position.y;

        return _host.transform.DOJump(jumpEndpoint, _jumpPower, 1, _moveDuration)
            .SetEase(Ease.InOutQuad);
    }

    public void OnExit()
    {
    }
}