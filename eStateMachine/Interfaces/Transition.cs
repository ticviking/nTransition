using System;

namespace eStateMachine.Interfaces
{
    public interface Transition<TState> where TState : IComparable
    {
        TState FromState { get; }
        TState ToState { get; }

        /// <summary>
        /// Set the destination state of the transition
        /// </summary>
        /// <param name="to">The Destination of the transiton</param>
        void To(TState to);

        /// <summary>
        /// Set the origin state of a transtion
        /// </summary>
        /// <param name="fromState">The Origin of the transition</param>
        void From(TState fromState);

        /// <summary>
        /// Attempt to finalize the transition.
        /// </summary>
        /// <returns>The Finalized EdgeTransition</returns>
        EdgeTransition<TState> Done();
    }
}