using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eStateMachine
{
    public class TransitionMachine<TState> where TState: IComparable
    {
        public TransitionMachine(Action<StateMachineConfig<TState>> action)
        {
            var config = new StateMachineConfig<TState>();
            action(config);
            Configuration = config;
        }

        public Boolean Configured { get { return Configuration != null; } }
        private StateMachineConfig<TState> Configuration { get; set; }
        public IEnumerable<TState> States { get { return Configuration.States; } }

        public TState Set(TState current, TState newState)
        {
            return Configuration.Set(current, newState);
        }
    }

    public class StateMachineConfig<TState> where TState: IComparable 
    {
        private IList<StateTransition<TState>> _stateTransitions;
        private StateTransition<TState> _inProgressTransition;

        public StateMachineConfig()
        {
            _stateTransitions = new List<StateTransition<TState>>();
            _inProgressTransition = new StateTransition<TState>();
        }

        public IEnumerable<TState> States
        {
            get
            {
               return _stateTransitions.Select(s => s.WhenState).Union(_stateTransitions.Select(s => s.ToState)).Distinct();
            }
        }

        public StateMachineConfig<TState> From(TState whenState)
        {
            _inProgressTransition.When(whenState);
            return this;
        }

        public StateMachineConfig<TState> To(TState toState)
        {
            _inProgressTransition.To(toState);
            return this;
        }

        public void Done()
        {
            var transition = _inProgressTransition.Done();
            if(transition != null) _stateTransitions.Add(transition);
            _inProgressTransition = new StateTransition<TState>();
        }

        public TState Set(TState current, TState newState)
        {
            var stateTransitions = _stateTransitions.Where(s => s.WhenState.CompareTo(current) == 0 && s.ToState.CompareTo( newState) == 0 );
            if (!stateTransitions.Any() ) throw new InvalidTransitionException("No Such State Transition Exists");

            return newState;
        }
    }

    public class StateTransition<TState> where TState : IComparable 
    {
        public TState WhenState { get; set; }
        private bool _hasWhened = false;
        public TState ToState { get; set; }
        private bool _hasToed = false;

        public void To(TState to)
        {
            ToState = to;
            _hasToed = true;
        }

        public void When(TState whenState)
        {
            WhenState = whenState;
            _hasWhened = true;
        }

        public StateTransition<TState> Done()
        {
            if (!_hasWhened && !_hasToed) return null;
            return this;
        }
    }
}
