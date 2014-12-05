using System;
using nTransition.Interfaces;

namespace nTransition
{
    public class StateTransitionBuilder<TInput, TState> : TransitionConfigBuilder<TState> 
        where TState : IComparable
        where TInput : IComparable
    {
        private StateTransition<TInput, TState> _currentStateTransition;
        private StateTransition<TInput, TState>[] _stateTansitions;

        public StateTransitionBuilder<TInput,TState> On(TInput input)
        {
            _currentStateTransition.On(input);
            return this;
        }

        public StateTransitionBuilder<TInput, TState> On(TInput[] input)
        {
            foreach (var c in input)
                On(c);
            return this;
        }

    }
}
