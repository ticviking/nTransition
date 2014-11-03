using System.Runtime.Remoting.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using eStateMachine;
using Shouldly;

namespace eStateMachineTests
{
    [TestFixture]
    class UseAStateMachineTests
    {
        internal class UsesAStateMachine
        {
            private readonly StateMachine<int> Machine;
            private int _state;

            public UsesAStateMachine(StateMachine<int> machine)
            {
                Machine = machine;
            }

            public int State
            {
                get { return _state; }
                set { _state = Machine.Set(_state,value); }
            }
        }

        [Test]
        public void EmptyStateMachineThrowsOnAllSets()
        {
            var TestMachine = new StateMachine<int>(c => c.Done());
            var t = new UsesAStateMachine(TestMachine);
            Should.Throw<InvalidTransitionException>(() => t.State = 1);
        }
    }
}
