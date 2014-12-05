using System;
using System.Threading.Tasks;
using nTransition;
using NUnit.Framework;
using Shouldly;

namespace nTransitionTests
{
    [TestFixture]
    class ExampleTurnStileTests
    {
        /// <summary>
        /// This class uses nTransition to implement the turnstile example 
        /// given at http://en.wikipedia.org/wiki/Finite-state_machine
        /// </summary>
        class TurnStile
        {
            public enum TurnstileState
            {
                Locked,
                Unlocked
            }
            private static readonly TransitionMachine<TurnstileState> Machine = new TransitionMachine<TurnstileState>(c =>
            {
                c.From(TurnstileState.Locked).To(TurnstileState.Unlocked).Done();
                c.From(TurnstileState.Unlocked).To(TurnstileState.Locked).Done();
            });
            private TurnstileState _status;

            public TurnstileState Status
            {
                get { return _status; }
                private set { _status = Machine.Between(_status, value); }
            }

            public void Push()
            {
                if (Status == TurnstileState.Locked) throw new Exception("You Can't Enter a Locked Turnstile");
                Status = TurnstileState.Locked;
            }

            public void InsertCoin()
            {
                Status = TurnstileState.Unlocked;
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
            Should.NotThrow(() => theTurnStile.InsertCoin());
            theTurnStile.Status.ShouldBe(TurnStile.TurnstileState.Unlocked);
        }

        [Test]
        public void UnlockedActions()
        {
            // Setup
            theTurnStile.InsertCoin(); // Unlock the turnstile

            // Test
            Should.Throw<Exception>(() => theTurnStile.InsertCoin());
            Should.NotThrow(() => theTurnStile.Push());
            theTurnStile.Status.ShouldBe(TurnStile.TurnstileState.Locked);
        }
    }
}
