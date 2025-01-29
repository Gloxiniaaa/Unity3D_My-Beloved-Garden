using UnityEngine;

public class UseShovelState : IState
{
    private readonly Player _host;
    private readonly Animator _animator;
    private readonly int _triggerAnimHash = Animator.StringToHash("useShovel");

    public UseShovelState(Player host, Animator animator)
    {
        _host = host;
        _animator = animator;
    }

    public void OnEnter()
    {
        RemovePlant();
        _host.transform.LookAt(_host.transform.position + _host.TargetDirection);
        _animator.SetTrigger(_triggerAnimHash);
    }

    private void RemovePlant()
    {
        if (Physics.Raycast(_host.transform.position, _host.TargetDirection, out RaycastHit hit, 1f, Constant.FLOWER_LAYER_MASK))
        {
            hit.collider.GetComponent<Flower>().ReverseBloom();
        }
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}