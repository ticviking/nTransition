using System;
using System.Collections.Generic;
using System.Text;

namespace eStateMachine
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

        private TransitionConfiguration<TState> Configuration { get; set; }
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
    }
}
