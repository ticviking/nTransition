using System;
using eStateMachine.Interfaces;

namespace eStateMachine
{
    /// <summary>
    /// Concrete implementation of a 
    /// </summary>
    /// <typeparam name="TState">Type representing the States being transiutioned between</typeparam>
    public class Transition<TState> : ITransition<TState> where TState : IComparable 
    {
        public TState FromState { get; private set; }
        private bool _hasAssignedFrom = false;
        public TState ToState { get; private set; }
        private bool _hasAssignedTo = false;

        /// <summary>
        /// Set the destination state of the transition
        /// </summary>
        /// <param name="to">The Destination of the transiton</param>
        public void To(TState to)
        {
            ToState = to;
            _hasAssignedTo = true;
        }

        /// <summary>
        /// Set the origin state of a transtion
        /// </summary>
        /// <param name="fromState">The Origin of the transition</param>
        public void From(TState fromState)
        {
            FromState = fromState;
            _hasAssignedFrom = true;
        }

        /// <summary>
        /// Attempt to finalize the transition.
        /// </summary>
        /// <returns>The Finalized Transition</returns>
        public Transition<TState> Done()
        {
            if (!IsValid) throw new InvalidTransitionException("Attempted to finalize an incomplete transition");
            return this;
        }

        private bool IsValid
        {
            get { return _hasAssignedFrom && _hasAssignedTo; }
        }
    }
}