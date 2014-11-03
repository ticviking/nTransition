using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eStateMachine
{
    public class StateMachine
    {
        public StateMachine(Action<StateMachineConfig> action)
        {
            var config = new StateMachineConfig();
            action(config);
            Configuration = config;
        }

        public Boolean Configured { get { return Configuration != null; } }
        private StateMachineConfig Configuration { get; set; }
    }

    public class StateMachineConfig
    {
        public StateConfig When(int i)
        {
            return new StateConfig
            {
                When = i
            };
        }
    }

    public struct StateConfig
    {
        public int When { get; set; }

    }
}
