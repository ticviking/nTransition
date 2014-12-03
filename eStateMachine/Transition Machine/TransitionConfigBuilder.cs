using System;
using System.Collections.Generic;
using System.Linq;
using eStateMachine.Interfaces;

namespace eStateMachine
{
    public class TransitionConfigBuilder<TState> where TState: IComparable 
    {
        private IList<Transition<TState>> _stateTransitions;
        private Transition<TState> _inProgressTransition;

        public TransitionConfigBuilder()
        {
            _stateTransitions = new List<Transition<TState>>();
            _inProgressTransition = new EdgeTransition<TState>();
        }

        public IEnumerable<TState> States
        {
            get
            {
                return _stateTransitions.Select(s => s.FromState).Union(_stateTransitions.Select(s => s.ToState)).Distinct();
            }
        }

        public TransitionConfigBuilder<TState> From(TState whenState)
        {
            _inProgressTransition.From(whenState);
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
            _inProgressTransition = new EdgeTransition<TState>();
        }

        public TState Between(TState current, TState newState)
        {
            var stateTransitions = _stateTransitions.Where(s => s.FromState.CompareTo(current) == 0 && s.ToState.CompareTo( newState) == 0 );
            if (!stateTransitions.Any() ) throw new InvalidTransitionException("No Such State EdgeTransition Exists");

            return newState;
        }

        public TransitionConfiguration<TState> GetConfiguration()
        {
            return new TransitionConfiguration<TState>(_stateTransitions);
        }

        public TransitionConfigBuilder<TState> If(Transition<TState>.IfPredicate predicate)
        {
            _inProgressTransition.If(predicate);
            return this;
        }

        public TransitionConfigBuilder<TState> Then(Transition<TState>.ThenClause func)
        {
            _inProgressTransition.Then(func);
            return this;
        }
    }
}