using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eStateMachine
{
    public class TransitionMachine<TState> where TState: IComparable
    {
        public TransitionMachine(Action<TransitionConfigBuilder<TState>> action)
        {
            var config = new TransitionConfigBuilder<TState>();
            action(config);
            Configuration = config;
        }

        private TransitionConfigBuilder<TState> Configuration { get; set; }
        public IEnumerable<TState> States { get { return Configuration.States; } }

        /// <summary>
        /// Run a transition between the given states
        /// </summary>
        /// <param name="fromState">The Start State for this transition</param>
        /// <param name="toState">The Destination State for this transition</param>
        /// <returns>The final state after this transition</returns>
        public TState Between(TState fromState, TState toState)
        {
            return Configuration.Between(fromState, toState);
        }
    }

    public class TransitionConfigBuilder<TState> where TState: IComparable 
    {
        private IList<StateTransition<TState>> _stateTransitions;
        private StateTransition<TState> _inProgressTransition;

        public TransitionConfigBuilder()
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

        public TransitionConfigBuilder<TState> From(TState whenState)
        {
            _inProgressTransition.When(whenState);
            return this;
        }

        public TransitionConfigBuilder<TState> To(TState toState)
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

        public TState Between(TState current, TState newState)
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
