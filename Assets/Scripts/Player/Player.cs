using UnityEngine;

public class Player : MonoBehaviour
{
    private Collider _collider;
    public Vector3 TargetDirection { get; private set; } // read by states, set by controller
    private IdleState _idle; // default state, some states will automatically exit when their animations end, then the defualt state will be set 
    private MoveState _move;
    private ReverseMoveState _reverseMove;
    public IState CurrentState { get; private set; }

    [Header("Broadcast on:")]
    [SerializeField] private Vec3EventChannelSO _spawnFlowerChannel;

    private void Awake()
    {
        Animator animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        _idle = new();
        _move = new(this, animator);
        _reverseMove = new(this, animator);
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
        TargetDirection = direction;
        SwitchState(_move);
    }

    public void ReverseMove(Vector3 direction)
    {
        TargetDirection = direction;
        SwitchState(_reverseMove);
    }

    // private void Update() => CurrentState.Tick();

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Constant.LAND_LAYER && CurrentState == _move)
        {
            _spawnFlowerChannel.RaiseEvent(other.transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Constant.FLOWER_LAYER)
        {
            if (CurrentState == _reverseMove)
            {
                other.gameObject.GetComponent<Flower>().ReverseBloom();
            }
            else if (CurrentState == _move)
            {
                Debug.Log("You stepped on a flower!!!");
            }
        }
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