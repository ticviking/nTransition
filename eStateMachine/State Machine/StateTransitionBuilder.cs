using System;

namespace eStateMachine
{
    public class StateTransitionBuilder<TInput, TState> : TransitionConfigBuilder<TState> 
        where TState : IComparable
        where TInput : IComparable
    {
        public StateTransitionBuilder<TInput,TState> On(TInput input)
        {
            throw new NotImplementedException();
        }

        public StateTransitionBuilder<TInput, TState> On(char[] input)
        {
            foreach (char c in input)
                On(c);
            return this;
        }

        private void On(char input)
        {
            throw new NotImplementedException();
        }
    }
}