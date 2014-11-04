using System;
using System.Collections.Generic;
using System.Linq;

namespace eStateMachine
{
    public class TransitionConfiguration<TState> where TState : IComparable
    {
        private readonly IList<StateTransition<TState>> _stateTransitions;

        public TransitionConfiguration(IList<StateTransition<TState>> stateTransitions)
        {
            _stateTransitions = stateTransitions;
        }

        public IEnumerable<TState> States
        {
            get
            {
                return _stateTransitions.Select(s => s.WhenState).Union(_stateTransitions.Select(s => s.ToState)).Distinct();
            }
        }

        public TState Between(TState current, TState newState)
        {
            var stateTransitions = _stateTransitions.Where(s => s.WhenState.CompareTo(current) == 0 && s.ToState.CompareTo( newState) == 0 );
            if (!stateTransitions.Any() ) throw new InvalidTransitionException("No Such State Transition Exists");

            return newState;
        }
    }
}