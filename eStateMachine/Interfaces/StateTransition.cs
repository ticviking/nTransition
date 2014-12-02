using System;

namespace eStateMachine.Interfaces
{
    public abstract class StateTransition<TInput, TState> : EdgeTransition<TState> where TState : IComparable
    {
        public abstract void On(TInput input);
    }
}
