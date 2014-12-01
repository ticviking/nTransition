using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using eStateMachine.Interfaces;
using NUnit.Framework;
using eStateMachine;
using Shouldly;

namespace eStateMachineTests
{
    [TestFixture]
    class UseAStateMachineTests
    {
        internal class UsesAStateMachine<T> where T : IComparable
        {
            private readonly TransitionMachine<T> Machine;
            private T _state;

            public UsesAStateMachine(TransitionMachine<T> machine)
            {
                Machine = machine;
            }

            public UsesAStateMachine(TransitionMachine<T> machine, T initialState)
            {
                Machine = machine;
                _state = initialState;
            }

            public T State
            {
                get { return _state; }
                set { _state = Machine.Between(_state,value); }
            }
        }

        [Test]
        public void EmptyStateMachineThrowsOnAllSets()
        {
            var TestMachine = new TransitionMachine<int>(new TransitionConfiguration<int>(new List<Transition<int>>()));
            var t = new UsesAStateMachine<int>(TestMachine);
            Should.Throw<InvalidTransitionException>(() => t.State = 1);
            // Try the default type. Important since we were adding a Default -> Default transition in the default constructor
            Should.Throw<InvalidTransitionException>(() => t.State = 0);
        }

        [Test]
        public void SimpleCircularStateMachineDoesNotThrow()
        {
            var TestMachine = new TransitionMachine<int>(c =>
            {
                c.From(1).To(2).Done();
                c.From(2).To(3).Done();
                c.From(3).To(1).Done();
            });
            var t = new UsesAStateMachine<int>(TestMachine, 1);
            Should.NotThrow(() =>
            {
                t.State = 2;
                t.State = 3;
                t.State = 1;
                t.State = 2;
                t.State = 3;
                t.State = 1;
            });
        }
    }
}
