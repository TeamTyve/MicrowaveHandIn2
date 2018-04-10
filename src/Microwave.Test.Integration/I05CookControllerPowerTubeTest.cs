using System;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class I05CookControllerPowerTubeTest
    {
        private CookController input;
        private ITimer timer;
        private IPowerTube powerTube;
        private IOutput output;
        private IDisplay display;
        private IUserInterface ui;

        [SetUp]
        public void Setup()
        {
            timer = Substitute.For<ITimer>();
            output = Substitute.For<IOutput>();
            display = Substitute.For<IDisplay>();
            ui = Substitute.For<IUserInterface>();
            powerTube = new PowerTube(output);
            input = new CookController(timer, display, powerTube, ui);
        }

        [TestCase(1, "1")]
        [TestCase(700, "700")]
        public void CookController_StartCooking_Returns(int received, string expected)
        {
            input.StartCooking(received, 30);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains(expected)));
        }

        [TestCase(0, "0")]
        [TestCase(701, "701")]
        public void CookController_StartCooking_DoesntReturn(int received, string expected)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => input.StartCooking(received, 30));
        }

        [Test]
        public void CookController_StopCooking_Returns()
        {
            input.StartCooking(1, 30);
            input.Stop();

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void CookController_StopCooking_DoesntReturn()
        {
            input.Stop();

            output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void CookController_OnTimerExpired_Returns()
        {
            input.StartCooking(1, 30);
            input.OnTimerExpired(new object(), EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void CookController_OnTimerExpired_DoesntReturn()
        {
            input.OnTimerExpired(new object(), EventArgs.Empty);

            output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }






    }
}