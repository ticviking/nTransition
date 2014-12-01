using System;

namespace eStateMachine.Interfaces
{
    public abstract class Transition<TState> where TState : IComparable
    {
        public delegate bool TestDelagate();

        public abstract TState FromState { get; }
        public abstract TState ToState { get; }

        /// <summary>
        /// Set the destination state of the transition
        /// </summary>
        /// <param name="to">The Destination of the transiton</param>
        public abstract void To(TState to);

        /// <summary>
        /// Set the origin state of a transtion
        /// </summary>
        /// <param name="fromState">The Origin of the transition</param>
        public abstract void From(TState fromState);

        /// <summary>
        /// Attempt to finalize the transition.
        /// </summary>
        /// <returns>The Finalized EdgeTransition</returns>
        public abstract EdgeTransition<TState> Done();
    }
}