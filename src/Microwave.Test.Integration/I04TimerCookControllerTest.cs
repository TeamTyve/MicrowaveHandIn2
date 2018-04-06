using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class I04TimerCookControllerTest
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
            timer = new Timer();
            output = Substitute.For<IOutput>();
            powerTube = new PowerTube(output);
            display = new Display(output);
            ui = Substitute.For<IUserInterface>();
            input = new CookController(timer, display, powerTube, ui);
        }

        [Test]
        public void OnTimerTick_TimeRemaining_Display()
        {
            input.StartCooking(50, 100);
            input.OnTimerTick(new object(), EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Display")));
        }

        [Test]
        public void OnTimerExpired_TimerOn_Returns()
        {
            input.StartCooking(50, 50);
            input.OnTimerExpired(new object(), EventArgs.Empty);

            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void StopCooking_ResultIsFunctionsCalled()
        {
            input.Stop();
            timer.Received(1).Stop();
            powerTube.Received(1).TurnOff();
        }

    }
}
