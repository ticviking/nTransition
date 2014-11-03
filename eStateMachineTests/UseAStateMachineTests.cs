using System;
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
        internal class UsesAStateMachine<T> where T :IEquatable<T>
        {
            private readonly StateMachine<T> Machine;
            private T _state;

            public UsesAStateMachine(StateMachine<T> machine)
            {
                Machine = machine;
            }

            public T State
            {
                get { return _state; }
                set { _state = Machine.Set(_state,value); }
            }
        }

        [Test]
        public void EmptyStateMachineThrowsOnAllSets()
        {
            var TestMachine = new StateMachine<int>(c => c.Done());
            var t = new UsesAStateMachine<int>(TestMachine);
            Should.Throw<InvalidTransitionException>(() => t.State = 1);
            // Try the default type. Important since we were adding a Default -> Default transition in the default constructor
            Should.Throw<InvalidTransitionException>(() => t.State = 0);
        }
    }
}
