using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eStateMachine
{
    public class StateMachine<TState> where TState: IEquatable<TState>
    {
        public StateMachine(Action<StateMachineConfig<TState>> action)
        {
            var config = new StateMachineConfig<TState>();
            action(config);
            Configuration = config;
        }

        public Boolean Configured { get { return Configuration != null; } }
        private StateMachineConfig<TState> Configuration { get; set; }
        public IEnumerable<TState> States { get { return Configuration.States; } }

        public TState Set(TState current, TState newState)
        {
            return Configuration.Set(current, newState);
        }
    }

    public class StateMachineConfig<TState> where TState: IEquatable<TState>
    {
        private IList<StateTransition<TState>> _stateTransitions;
        private StateTransition<TState> _inProgressTransition;

        public StateMachineConfig()
        {
            _stateTransitions = new List<StateTransition<TState>>();
        }

        public IEnumerable<TState> States
        {
            get
            {
               return _stateTransitions.Select(s => s.WhenState).Union(_stateTransitions.Select(s => s.ToState)).Distinct();
            }
        }

        public StateMachineConfig<TState> When(TState i)
        {
            _inProgressTransition = new StateTransition<TState>
            {
                WhenState = i
            };
            return this;
        }

        public StateMachineConfig<TState> To(TState toState)
        {
            _inProgressTransition.To(toState);
            return this;
        }

        public void Done()
        {
            _stateTransitions.Add(_inProgressTransition);
            _inProgressTransition = new StateTransition<TState>();
        }

        public TState Set(TState current, TState newState)
        {
            var stateTransitions = _stateTransitions.Where(s => Equals(s.WhenState, current) && Equals(s.ToState, newState));
            if (stateTransitions.Count() == 0 ) throw new InvalidTransitionException("No Such State Transition Exists");

            return newState;
        }
    }

    public struct StateTransition<TState> where TState : IEquatable<TState> 
    {
        public TState WhenState { get; set; }
        public TState ToState { get; set; }

        public void To(TState to)
        {
            ToState = to;
        }
    }
}
