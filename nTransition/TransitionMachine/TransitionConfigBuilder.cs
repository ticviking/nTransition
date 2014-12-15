using System;
using System.Collections.Generic;
using nTransition.Interfaces;

namespace nTransition
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

        public TransitionConfiguration<TState> GetConfiguration()
        {
            return new TransitionConfiguration<TState>(_stateTransitions);
        }

        public TransitionConfigBuilder<TState> When(Transition<TState>.IfPredicate predicate)
        {
            _inProgressTransition.When(predicate);
            return this;
        }

        public TransitionConfigBuilder<TState> Do(Transition<TState>.ThenClause func)
        {
            _inProgressTransition.Do(func);
            return this;
        }
    }
}
