using System.Security.Cryptography.X509Certificates;
using eStateMachine;
using Moq;
using NUnit.Framework;

namespace eStateMachineTests
{
    [TestFixture]
    public class ReadMeExamplesTest
    {
        private IUserService UserService;
        public ILog LoggingService { get; private set; }

        [TestFixtureSetUp]
        public void Setup()
        {
            var mock = new Mock<IUserService>();
            mock.Setup(e => e.CurrentUser.IsAdmin).Returns(true);
            UserService = mock.Object;
            var lmock = new Mock<ILog>();
            LoggingService = lmock.Object;
        }

        [Test]
        public void ExampleCreateTranstionMachine()
        {
            var trasitionMachine = new TransitionMachine<int>( (config) => {
                    // Defines a valid transition from 1 to 2
                    config.From(1).To(2).Done();
                    // Defines a transition from 2 to 3 if the current user is admin
                    config.From(2).To(3).When(()=> UserService.CurrentUser.IsAdmin).Done();
                    // Defines a transition from 3 to 1 that logs the reset of the machine to a log function
                    config.From(3).To(1).Do( () => LoggingService.Log("The statemachine has been reset")).Done();
                  });
        }

        [Test]
        [Ignore] // On Hold to finish needed features for eGrad
        public void ExampleCreateStateMachine()
        {
            var fm = new StateMachine<char, int>( (config) => {
                // The On method defines what input is acceptable
                config.On('a').From(1).To(2).Done();
                // On can also accept an Ienumerable of the input type.
                // State Machines can use both When, and Do clauses to check conditions or fire events when on a particular transition 
                config.On( new []{'b', 'c'}).From(2).To(3).When( () => UserService.CurrentUser.IsAdmin).Done();
              });
        }

    }

    public interface ILog
    {
        void Log(string s);
    }

    public interface IUserService
    {
        IHasIsAdmin CurrentUser { get; set; }
    }

    public interface IHasIsAdmin
    {
        bool IsAdmin { get; set; }
    }
}
