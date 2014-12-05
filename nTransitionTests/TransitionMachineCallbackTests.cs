using System;
using System.Runtime.InteropServices;
using nTransition;
using nTransition.Interfaces;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace nTransitionTests
{
    [TestFixture]
    public class TransitionMachineCallbackTests
    {


        [Test]
        public void IfClauses()
        {
            var machine = new TransitionMachine<int>(c =>
            {
                // Only one of these is true
                c.From(1).To(2).When(() => 1>2).When(() => 1<2).Done();
                c.From(2).To(3).When(()=> 1<2).When(()=> 2<1).Done();
                // Both of these are true
                c.From(2).To(1).When(()=> 1<2 ).When(()=> 2<3).Done();
            });

            Should.Throw<InvalidTransitionException>(() => machine.Between(1, 2));
            Should.Throw<InvalidTransitionException>(() => machine.Between(2, 3));
            Should.NotThrow(() => machine.Between(2, 1));
            
        }

        [Test]
        public void ThenClauses()
        {
            bool called = false;
            var machine = new TransitionMachine<int>(c => c.From(1).To(2).Do(()=> called = true).Done());

            machine.Between(1, 2);
            called.ShouldBe(true);
        }
    }

}
