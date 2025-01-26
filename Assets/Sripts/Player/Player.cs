using System;
using DG.Tweening;
using FSM;
using GameInput;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader;
    [SerializeField] private float _gridSize;
    public Vector3 TargetDirection { get; private set; }

    private readonly StateMachine _stateMachine = new();
    private IdleState _idle; //default state, some states will automatically exit when their animations end, then the defualt state will be set 

    void At(IState from, IState to) => _stateMachine.AddTransition(from, to);


    private void Awake()
    {
        Animator animator = GetComponent<Animator>();
        _idle = new();
        MoveForwardState moveForward = new(this.gameObject, animator);
        MoveState move = new(this, animator);

        At(moveForward, _idle);
        At(_idle, moveForward);

        At(move, _idle);
        At(_idle, move);

        _stateMachine.SetState(_idle);
    }


    private void OnEnable()
    {
        // _inputReader.Left += OnLeft;
        // _inputReader.Right += OnRight;
        _inputReader.Move += Move;
    }

    private void Move(Vector2 direction)
    {
        if (direction.x != 0 && direction.y != 0)
            return;

        if (direction.x != 0)
        {
            TargetDirection = (Vector3)direction;
            _stateMachine.RequestSwitchState(typeof(MoveState));
        }
        if (direction.y != 0)
        {
            TargetDirection = new Vector3(0, 0, direction.y);
            _stateMachine.RequestSwitchState(typeof(MoveState));
        }

    }

    private void Update() => _stateMachine.Tick();

    private void OnRight()
    {
        // transform.DOMove(transform.position + Vector3.right * _gridSize, 0.5f);
        _stateMachine.RequestSwitchState(typeof(MoveForwardState));
    }

    private void OnLeft()
    {
        TargetDirection = Vector3.left;
        _stateMachine.RequestSwitchState(typeof(MoveState));
    }

    private void OnDisable()
    {
        // _inputReader.Left -= OnLeft;
        // _inputReader.Right -= OnRight;
        _inputReader.Move -= Move;
    }

    // called by animation event
    private void OnExitState()
    {
        _stateMachine.SetState(_idle);
    }
}