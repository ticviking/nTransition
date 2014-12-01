using eStateMachine;
using NUnit.Framework;
using Shouldly;

namespace eStateMachineTests
{
    [TestFixture]
    public class TransitionMachineCallbackTests
    {
        [Test]
        public void MachinesUseIfTransitions()
        {
            var machine = new TransitionMachine<int>((c) =>
            {
                c.From(1).To(2).If(() => 1>2).Done();
                c.From(2).To(1).If(()=> 1<2 ).Done();
            });

            Should.Throw<InvalidTransitionException>(() => machine.Between(1, 2));
            Should.NotThrow(() => machine.Between(2, 1));
        }
    }
}