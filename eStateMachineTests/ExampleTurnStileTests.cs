using eStateMachine;
using NUnit.Framework;

namespace eStateMachineTests
{
    [TestFixture]
    class ExampleTurnStileTests
    {
        /// <summary>
        /// This class uses eStateMachine to implement the turnstile example 
        /// given at http://en.wikipedia.org/wiki/Finite-state_machine
        /// </summary>
        class TurnStile
        {
            enum TurnstileStates
            {
                Locked,
                Unlocked
            }
            private static readonly StateMachine<TurnstileStates> Machine = new StateMachine<TurnstileStates>(c => { });
        }
        
    }
}