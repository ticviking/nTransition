using System;
using System.Linq;
using System.Reflection;

namespace eStateMachine.Interfaces
{
    public abstract class Transition<TState> where TState : IComparable
    {
        /// <summary>
        /// Holds the callbacks passed to When. 
        /// </summary>
        /// <returns></returns>
        public delegate bool IfPredicate();
        public delegate void ThenClause();

        private IfPredicate _constraintPredicates;
        private ThenClause _Callbacks;

        public bool PassesConstraints
        {
            get
            {
                if (_constraintPredicates == null) return true;
                return _constraintPredicates.GetInvocationList().Select<Delegate, bool>((Delegate a) =>
                {
                    bool r = (bool) a.DynamicInvoke();
                    return r;
                }).Aggregate((a, b) => a && b);
            }
        }

        public abstract TState FromState { get; set; }
        public abstract TState ToState { get; set; }

        public void When(IfPredicate t)
        {
            _constraintPredicates += t;
        }

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
        public abstract Transition<TState> Done();

        public void Do(ThenClause func)
        {
            _Callbacks += func;
        }

        public void runCallbacks()
        {
            if (_Callbacks == null) return;
            _Callbacks();
        }
    }
}