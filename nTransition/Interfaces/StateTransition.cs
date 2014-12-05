using System;

namespace nTransition.Interfaces
{
    public abstract class StateTransition<TInput, TState> : EdgeTransition<TState> where TState : IComparable
    {
        public abstract void On(TInput input);
    }
}
