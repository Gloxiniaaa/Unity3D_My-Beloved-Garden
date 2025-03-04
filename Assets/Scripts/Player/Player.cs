using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 TargetDirection { get; private set; } // read by states, set by controller
    private IdleState _idle; // default state, some states will automatically exit when their animations end, then the defualt state will be set 
    private MoveState _move;
    private ReverseMoveState _reverseMove;
    private UseShovelState _useShovel;
    public IState CurrentState { get; private set; }

    [Header("SFX")]
    [SerializeField] private AudioGroupSO _sfxJump;
    [SerializeField] private AudioGroupSO _sfxRewind;

    [Header("Broadcast on:")]
    [SerializeField] private Vec3EventChannelSO _onLandSlotPlantedChannel;
    [SerializeField] private AudioEventChannelSO _playSfxChannel;
    [SerializeField] private VoidEventChannelSO _onStepOnFlower;

    private void Awake()
    {
        Animator animator = GetComponent<Animator>();
        _idle = new();
        _move = new(this, animator);
        _reverseMove = new(this, animator);
        _useShovel = new(this, animator);

    }

    void OnEnable()
    {
        _move.OnStepOnFlower += OnStepOnFlower;
    }

    private void OnStepOnFlower()
    {
        _onStepOnFlower.RaiseEvent();
    }

    private void Start()
    {
        SwitchState(_idle);
    }

    public bool CanMove()
    {
        return CurrentState == _idle;
    }

    public void Move(Vector3 direction)
    {
        _playSfxChannel.RaiseEvent(_sfxJump);
        TargetDirection = direction;
        SwitchState(_move);
    }

    public void ReverseMove(Vector3 direction)
    {
        _playSfxChannel.RaiseEvent(_sfxRewind);
        TargetDirection = direction;
        SwitchState(_reverseMove);
    }

    public void UseShovel(Vector3 direction)
    {
        TargetDirection = direction;
        SwitchState(_useShovel);
    }

    // private void Update() => CurrentState.Tick();

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Constant.LAND_LAYER && CurrentState == _move)
        {
            _onLandSlotPlantedChannel.RaiseEvent(other.transform.position);
        }
    }

    private void OnDisable()
    {
        _move.OnStepOnFlower -= OnStepOnFlower;
    }

    // called by animation event
    private void OnExitState()
    {
        SwitchState(_idle);
    }

    private void SwitchState(IState newState)
    {
        CurrentState?.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }
}