using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eStateMachine
{
    public class StateMachine<TState>
    {
        public StateMachine(Action<StateMachineConfig<TState>> action)
        {
            var config = new StateMachineConfig<TState>();
            action(config);
            Configuration = config;
        }

        public Boolean Configured { get { return Configuration != null; } }
        private StateMachineConfig<TState> Configuration { get; set; }
    }

    public class StateMachineConfig<TState>
    {
        public StateConfig<TState> When(TState i)
        {
            return new StateConfig<TState>
            {
                When = i
            };
        }
    }

    public struct StateConfig<TState>
    {
        public TState When { get; set; }

    }
}
