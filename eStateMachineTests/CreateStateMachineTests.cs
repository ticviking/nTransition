using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using eStateMachine;
using Shouldly;

namespace eStateMachineTests
{
    [TestFixture]
    public class TransitionMachineTests
    {
        [Test]
        public void AcceptsAConfigAction()
        {
            var Machine = new TransitionMachine<int>((c) => c.From(1).To(2).Done());
        }

        [Test]
        public void CanUseDifferentTypesForStates()
        {
            var Machine = new TransitionMachine<string>(c => c.From("I").To("C").Done());
        }

        [Test]
        public void CanListThePossibleStates()
        {
            var Machine = new TransitionMachine<int>((c) =>
            {
                c.From(1).To(2).Done();
            });
            Machine.States.Count().ShouldBe(2);
            Machine.States.ShouldBe(new []{1,2});
        }
    }
}
