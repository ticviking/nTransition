using System;
using eStateMachine;
using NUnit.Framework;
using Shouldly;

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
            public enum TurnstileState
            {
                Locked,
                Unlocked
            }
            private static readonly StateMachine<TurnstileState> Machine = new StateMachine<TurnstileState>(c => { });
            private TurnstileState _status;

            public TurnstileState Status
            {
                get { return _status; }
                set { _status = Machine.Set(_status, value); }
            }

            public void Push()
            {
                if (Status == TurnstileState.Locked) throw new Exception("You Can't Enter a Locked Turnstile");
            }
        }


        private TurnStile theTurnStile;

        [SetUp]
        public void Setup()
        {
            theTurnStile = new TurnStile();
        }

        [Test]
        public void TurnStileStartsLocked()
        {
            theTurnStile.Status.ShouldBe(TurnStile.TurnstileState.Locked);
        }

        [Test]
        public void LockedActions()
        {
            Should.Throw<Exception>(() =>
            {
                theTurnStile.Push();
            });
        }
    }
}