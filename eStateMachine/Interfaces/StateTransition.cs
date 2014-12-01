using System;

namespace eStateMachine.Interfaces
{
    interface StateTransition<TInput, TState> : Transition<TState> where TState : IComparable
    {
        void On(TInput input);
    }
}
