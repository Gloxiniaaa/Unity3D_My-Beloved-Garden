using DG.Tweening;
using UnityEngine;

public class ReverseMoveState : IState
{
    private readonly Player _host;
    private readonly Animator _animator;
    private readonly float _moveDuration = 0.3f;
    private readonly float _jumpPower = 0.5f;
    private readonly int _boolMoveAnimHash = Animator.StringToHash("isMoving");
    private Tween _currentRotationTween;

    public ReverseMoveState(Player host, Animator animator)
    {
        _host = host;
        _animator = animator;
    }

    public void Tick()
    {
    }

    public void OnEnter()
    {
        _animator.SetBool(_boolMoveAnimHash, true);
        
        ReverseFlowerBloom();
        
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(_moveDuration);
        sequence.Join(RotateToDirection(_host.TargetDirection));
        sequence.Join(JumpInOppositeDirection(_host.TargetDirection));
        sequence.AppendCallback(() => { _animator.SetBool(_boolMoveAnimHash, false); });
    }

    private void ReverseFlowerBloom()
    {
        if (Physics.Raycast(_host.transform.position, -_host.TargetDirection, out RaycastHit hit, 1f, Constant.FLOWER_LAYER_MASK))
        {
            hit.collider.GetComponent<Flower>().ReverseBloom();
        }
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
    public Tween JumpInOppositeDirection(Vector3 direction)
    {
        direction = -direction;

        direction.Normalize();

        Vector3 jumpEndpoint = _host.transform.position + direction * Constant.GRID_SIZE;

        jumpEndpoint.y = _host.transform.position.y;

        return _host.transform.DOJump(jumpEndpoint, _jumpPower, 1, _moveDuration)
            .SetEase(Ease.InOutQuad);
    }

    public void OnExit()
    {
    }
}