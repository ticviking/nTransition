using System;

namespace eStateMachine
{
    public class Transition<TState> where TState : IComparable
    {
        private TransitionConfigBuilder<TState> _transitionConfigBuilder;

        public Transition(TransitionConfigBuilder<TState> transitionConfigBuilder)
        {
            _transitionConfigBuilder = transitionConfigBuilder;
        }

        public TransitionConfigBuilder<TState> From(TState whenState)
        {
            _transitionConfigBuilder.From(whenState);
            return _transitionConfigBuilder;
        }

        public TransitionConfigBuilder<TState> To(TState toState)
        {
            _transitionConfigBuilder.To(toState);
            return _transitionConfigBuilder;
        }
    }
}