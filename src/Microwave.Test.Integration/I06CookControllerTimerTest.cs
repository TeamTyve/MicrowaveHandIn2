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
    class I06CookControllerTimerTest
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
        public void OnTimerExpired_TimerOn_Returns()
        {
            input.StartCooking(50, 50);
            input.OnTimerExpired(timer, EventArgs.Empty);

            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void StartCooking_DisplayTimeRemaining()
        {
            input.StartCooking(50, 21);

            Thread.Sleep(1000);

            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains($"Display shows: {(21 / 60) / 1000}:{(21 % 60) / 1000}")));
        }

        [Test]
        public void StopCooking_OnTimerExpired_DisplayTurnedOff()
        {
            input.StartCooking(50, 21);
            input.Stop();

            Thread.Sleep(1000);

            output.DidNotReceive().OutputLine(Arg.Is<string>(str => str.Contains($"Display shows: {(21 / 60) / 1000}:{(21 % 60) / 1000}")));
        }

        [Test]
        public void StartCooking_OnTimerTick_DisplayTimeRemaining()
        {
            input.StartCooking(50, 21);

            input.OnTimerTick(timer, EventArgs.Empty);

            output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains($"Display shows:")));
        }
    }
}
