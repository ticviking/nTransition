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
            var Machine = new StateMachine((c) =>
            {
                
            });
        }

        [Test]
        public void AcceptsAConfigAction()
        {
            var Machine = new StateMachine((c) =>
            {
                c.When(1);
            });

            Machine.Configured.ShouldBe(true);
        }
    }
}
