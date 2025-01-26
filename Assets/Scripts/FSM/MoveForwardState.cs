using UnityEngine;


public class MoveForwardState : IState
{
    private readonly GameObject _host;
    private Animator _animator;
    private bool _isMoving = false;
    private Vector3 _targetPos;
    private float _speed = 1.0f;
    private int _BoolMoveAnimHash = Animator.StringToHash("isMoving");

    public MoveForwardState(GameObject host, Animator animator)
    {
        _host = host;
        _animator = animator;
    }

    public void Tick()
    {
        _host.transform.position = Vector3.MoveTowards(_host.transform.position, _targetPos, _speed * Time.deltaTime);

        if ((_host.transform.position - _targetPos).magnitude <= 0.001f)
        {
            _host.transform.position = _targetPos;
            _animator.SetBool(_BoolMoveAnimHash, false);
        }
    }

    public void OnEnter()
    {
        if (_isMoving)
            return;
        _isMoving = true;
        _targetPos = _host.transform.position + Constant.GRID_SIZE * _host.transform.forward;
        _animator.SetBool(_BoolMoveAnimHash, true);
    }

    public void OnExit()
    {
        _isMoving = false;
    }
}