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
            var machine = new TransitionMachine<int>(c =>
            {
                c.From(1).To(2).If(() => 1>2).Done();
                c.From(2).To(1).If(()=> 1<2 ).Done();
            });

            Should.Throw<InvalidTransitionException>(() => machine.Between(1, 2));
            Should.NotThrow(() => machine.Between(2, 1));
        }

        [Test]
        public void MultipleIfsAreAllTested()
        {
            var machine = new TransitionMachine<int>(c =>
            {
                // Only one of these is true
                c.From(1).To(2).If(() => 1>2).If(() => 1<2).Done();
                c.From(2).To(3).If(()=> 1<2).If(()=> 2<1).Done();
                // Both of these are true
                c.From(2).To(1).If(()=> 1<2 ).If(()=> 2<3).Done();
            });

            Should.Throw<InvalidTransitionException>(() => machine.Between(1, 2));
            Should.Throw<InvalidTransitionException>(() => machine.Between(2, 3));
            Should.NotThrow(() => machine.Between(2, 1));
            
        }
    }
}