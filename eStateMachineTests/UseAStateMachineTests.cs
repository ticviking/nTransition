﻿using System;
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

            public UsesAStateMachine(StateMachine<T> machine, T initialState)
            {
                Machine = machine;
                _state = initialState;
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

        [Test]
        public void SimpleCircularStateMachineDoesNotThrow()
        {
            var TestMachine = new StateMachine<int>(c =>
            {
                c.When(1).To(2).Done();
                c.When(2).To(3).Done();
                c.When(3).To(1).Done();
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
