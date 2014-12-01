using eStateMachine;
using NUnit.Framework;

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
        }
    }
}