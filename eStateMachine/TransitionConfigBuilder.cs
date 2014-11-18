using System;
using System.Collections.Generic;
using System.Linq;

namespace eStateMachine
{
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

        public TransitionConfiguration<TState> GetConfiguration()
        {
            return new TransitionConfiguration<TState>(_stateTransitions);
        }

        public TransitionConfigBuilder<TState> If(Func<bool> predicate)
        {
            return this;
        }

        public TransitionConfigBuilder<TState> Then(Action func)
        {
            return this;
        }
    }
}