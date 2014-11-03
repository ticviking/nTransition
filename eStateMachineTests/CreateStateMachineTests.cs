using System;
using System.Configuration;
using System.Threading;
using NUnit.Framework;
using eStateMachine;
using Shouldly;

namespace eStateMachineTests
{
    [TestFixture]
    public class StateMachineTests
    {
        [Test]
        public void Exist()
        {
            var Machine = new StateMachine<int>((c) =>
            {

            });
        }

        [Test]
        public void AcceptsAConfigAction()
        {
            var Machine = new StateMachine<int>((c) =>
            {
                c.When(1); // ToDo: Make this a complete state build
            });

            Machine.Configured.ShouldBe(true);
        }

        [Test]
        public void CanUseDifferentTypesForStates()
        {
            var Machine = new StateMachine<string>(c =>
            {
                c.When("I");
            });

        }
    }
}
