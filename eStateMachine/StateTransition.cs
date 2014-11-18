using System;

namespace eStateMachine
{
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