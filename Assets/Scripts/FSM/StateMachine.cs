using System;
using System.Collections.Generic;

namespace FSM
{
    public class StateMachine
    {
        private IState _currentState;

        private Dictionary<Type, List<IState>> _transitions = new Dictionary<Type, List<IState>>();
        private List<IState> _currentTransitions = new List<IState>();
        private static List<IState> EmptyTransitions = new List<IState>(0);

        public void Tick() => _currentState?.Tick();

        public void SetState(IState state)
        {
            if (state == _currentState)
                return;

            _currentState?.OnExit();
            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
                _currentTransitions = EmptyTransitions;

            _currentState.OnEnter();
        }

        public void RequestSwitchState(IState next)
        {
            if (CanSwitch(next))
            {
                SetState(next);
            }
        }

        public void RequestSwitchState(Type next)
        {
            IState nextState = SearchState(next);
            if (nextState != null)
            {
                SetState(nextState);
            }
        }

        public void AddTransition(IState from, IState to)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitionsForThisType) == false)
            {
                transitionsForThisType = new List<IState>();
                _transitions[from.GetType()] = transitionsForThisType;
            }

            transitionsForThisType.Add(to);
        }

        private bool CanSwitch(IState next)
        {
            foreach (var transition in _currentTransitions)
            {
                if (transition == next)
                {
                    return true;
                }
            }
            return false;
        }

        private IState SearchState(Type next)
        {
            foreach (var transition in _currentTransitions)
            {
                if (transition.GetType() == next)
                {
                    return transition;
                }
            }
            return null;
        }
    }
}