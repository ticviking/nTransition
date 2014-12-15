using System;
using System.Collections.Generic;
using System.Linq;

namespace nTransition
{
    public class TransitionMachine<TState> where TState: IComparable
    {
        public TransitionMachine(Action<TransitionConfigBuilder<TState>> action)
        {
            var builder = new TransitionConfigBuilder<TState>();
            action(builder);
            Configuration = builder.GetConfiguration();
        }

        public TransitionMachine(TransitionConfiguration<TState> config)
        {
            Configuration = config;
        }

        protected TransitionMachine()
        {
            throw new NotImplementedException();
        }

        protected TransitionConfiguration<TState> Configuration { get; set; }
        public IEnumerable<TState> States { get { return Configuration.States; } }

        /// <summary>
        /// Run a transition between the given states
        /// </summary>
        /// <param name="fromState">The Start State for this transition</param>
        /// <param name="toState">The Destination State for this transition</param>
        /// <returns>The final state after this transition</returns>
        public TState Between(TState fromState, TState toState)
        {
            return Configuration.Between(fromState, toState);
        }


        /// <summary>
        /// Get possible states that can be transitioned to
        /// </summary>
        /// <param name="fromState">State to ne transitioned from</param>
        /// <returns>IEnumerable of states that can be transitioned to</returns>
        public IEnumerable<TState> GetTransitionsFromState(TState fromState)
        {
            //I'm not sure how efficient this is
            foreach (var stateTransition in Configuration.StateTransitions)
            {
                if (stateTransition.FromState.Equals(fromState))
                {
                    yield return stateTransition.ToState;
                }
            } 
        }
    }
}
