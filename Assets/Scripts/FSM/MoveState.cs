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
        RotateToDirection(_host.TargetDirection);
        JumpInDirection(_host.TargetDirection);

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(_moveDuration);
        sequence.Join(RotateToDirection(_host.TargetDirection));
        sequence.Join(JumpInDirection(_host.TargetDirection));
        sequence.AppendCallback(() => { _animator.SetBool(_BoolMoveAnimHash, false); });
    }

    public Tween RotateToDirection(Vector3 direction)
    {
        // 1. Calculate the target rotation (Quaternion)
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // 2. Calculate the delta rotation (Euler angles)
        Vector3 deltaRotation = targetRotation.eulerAngles - _host.transform.rotation.eulerAngles;

        // 3. Kill any existing tweens to prevent conflicts
        _currentRotationTween?.Kill();

        // 4. Create the blendable rotation tween
        _currentRotationTween = _host.transform.DOBlendableRotateBy(deltaRotation, _moveDuration)
            .SetEase(Ease.OutQuad)
            .SetUpdate(UpdateType.Normal, true); // Important for physics updates if needed.

        return _currentRotationTween;
    }
    public Tween JumpInDirection(Vector3 direction)
    {
        // 1. Normalize the direction vector to ensure a range of 1
        direction.Normalize();

        // 2. Calculate the jump endpoint by multiplying the direction by the desired range (1 in this case).
        Vector3 jumpEndpoint = _host.transform.position + direction;

        // 3. Keep the jump on the same horizontal plane (important!)
        jumpEndpoint.y = _host.transform.position.y;

        // 4. Kill any existing jump tweens
        // _host.transform.DOKill(true); // Kill all tweens on this transform

        // 5. Perform the jump using DOJump
        return _host.transform.DOJump(jumpEndpoint, _jumpPower, 1, _moveDuration)
            .SetEase(Ease.InOutQuad);
    }

    public void OnExit()
    {
    }
}