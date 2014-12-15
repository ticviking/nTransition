﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using nTransition.Interfaces;
using NUnit.Framework;
using nTransition;
using Shouldly;

namespace nTransitionTests
{
    [TestFixture]
    class UseATransitionMachineTests
    {
        private class User<T> where T : IComparable
        {
            private readonly TransitionMachine<T> Machine;
            private T _state;

            public User(TransitionMachine<T> machine)
            {
                Machine = machine;
            }

            public User(TransitionMachine<T> machine, T initialState)
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
        public void EmptyMachineThrowsOnAllSets()
        {
            var TestMachine = new TransitionMachine<int>(new TransitionConfiguration<int>(new List<Transition<int>>()));
            var t = new User<int>(TestMachine);
            Should.Throw<InvalidTransitionException>(() => t.State = 1);
            // Try the default type. Important since we were adding a Default -> Default transition in the default constructor
            Should.Throw<InvalidTransitionException>(() => t.State = 0);
        }

        [Test]
        public void SimpleCircularMachineDoesNotThrow()
        {
            var TestMachine = new TransitionMachine<int>(c =>
            {
                c.From(1).To(2).Done();
                c.From(2).To(3).Done();
                c.From(3).To(1).Done();
            });
            var t = new User<int>(TestMachine, 1);
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

        [Test]
        public void CanGetPossibleTransitionsFromState()
        {
            var TestMachine = new TransitionMachine<int>(c =>
            {
                c.From(1).To(2).Done();
                c.From(1).To(3).Done();
                c.From(2).To(4).Done();
                c.From(3).To(4).Done();
                c.From(4).To(1).Done();
            });
            var possibleTransitions = TestMachine.GetTransitionsFromState(1).ToArray();
            possibleTransitions.Length.ShouldBe(2);
            possibleTransitions[0].ShouldBe(2);
            possibleTransitions[1].ShouldBe(3);
        }
    }
}
